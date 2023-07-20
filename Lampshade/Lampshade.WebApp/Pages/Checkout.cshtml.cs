using System.Globalization;
using _0.Framework.Application.ZarinPal;
using Lampshade.Query.Contracts;
using Lampshade.Query.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace Lampshade.WebApp.Pages
{
	[Authorize]
	public class CheckoutModel : PageModel
	{
		public Cart Cart { get; set; }
		public const string CookieName = "cart-items";

		#region Constractor Injection

		private readonly ICartCalculateService _cartCalculateService;
		private readonly ICartService _cartService;
		private readonly IProductQuery _productQuery;
		private readonly IOrderApplication _orderApplication;

		public CheckoutModel(ICartCalculateService cartCalculateService, ICartService cartService,
			IProductQuery productQuery, IOrderApplication orderApplication)
		{
			_cartCalculateService = cartCalculateService;
			_cartService = cartService;
			_productQuery = productQuery;
			_orderApplication = orderApplication;
		}

		#endregion

		public void OnGet()
		{
			var serializer = new JavaScriptSerializer();
			var value = Request.Cookies[CookieName];

			var cartItems = serializer.Deserialize<List<CartItem>>(value);
			if (cartItems != null && cartItems.Any())
			{
				foreach (var item in cartItems)
				{
					item.CalculateTotalItemPrice();
				}

				Cart = _cartCalculateService.ComputeCart(cartItems);
				_cartService.Set(Cart);
			}
		}

		public async Task<ActionResult> OnPostPay(int paymentMethod)
		{
			var cart = _cartService.Get();

            cart.SetPaymentMethod(paymentMethod);

			var items = _productQuery.CheckInventoryStatus(cart.CartItems);

			if (items.Any(x => x.IsInStock == false))
			{
				return RedirectToPage("Cart");
			}

            var orderId = _orderApplication.PlaceOrder(cart);

            if (cart.PaymentMethod == ShopManagement.Domain.OrderAgg.PaymentMethod.OnlinePayment)
            {
                #region Online Payment

                var payAmount = cart.PayAmount.ToString(CultureInfo.InvariantCulture).Replace(",", "");
                var callBackUrl = $"https://localhost:7168/Checkout?handler=CallBack&oId={orderId}";
                var payment = new ZarinpalSandbox.Payment(int.Parse(payAmount));
                var response = await payment.PaymentRequest($"پرداخت فاکتور {orderId}", callBackUrl);
                if (response.Status == 100)
                {
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + response.Authority);
                }

                #endregion
            }
            else
            {
                var result = new PaymentResult();
                result = result.Succeeded("پرداخت شما به صورت نقدی بوده است و مبلغ هنگام تحویل کالا باید پرداخت شود",
                    "شماره پیگیری وجود ندارد");
                Response.Cookies.Delete(CookieName);
                return RedirectToPage("PaymentResult", result);
            }
            return Page();

			/*
			var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(),
				"09167584020", "alireza80heydri@gmail.com", $"{orderId} پرداخت فاکتور", orderId);

			return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
			*/
		}

		public async Task<IActionResult> OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
			[FromQuery] long oId)
		{
            var result = new PaymentResult();

            if (status != null && status.ToLower() == "ok" &&
				authority != null && !string.IsNullOrEmpty(authority) &&
				oId != 0)
			{
				var orderAmount = _orderApplication.GetAmountBy(oId)
					.ToString(CultureInfo.InvariantCulture)
					.Replace(",", "");

				var payment = new ZarinpalSandbox.Payment(int.Parse(orderAmount));
				var response = await payment.Verification(authority);

                if (response.Status == 100)
				{
					var issueTrackingNo = _orderApplication.PaymentSuccess(oId, response.RefId);
					Response.Cookies.Delete(CookieName);
					return RedirectToPage("PaymentResult",
						result.Succeeded($"پرداخت شما به قیمت {orderAmount} با موفقیت انجام شد",
						issueTrackingNo));
				}
            }

            return RedirectToPage("PaymentResult", result.Failed("خطا در پرداخت وجه"));
        }
	}
}
