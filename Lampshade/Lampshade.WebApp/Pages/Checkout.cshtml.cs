using Lampshade.Query.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace Lampshade.WebApp.Pages
{
    public class CheckoutModel : PageModel
    {
        public Cart Cart { get; set; }
        public const string CookieName = "cart-items";

        private readonly ICartCalculateService _cartCalculateService;
        public CheckoutModel(ICartCalculateService cartCalculateService)
        {
            _cartCalculateService = cartCalculateService;
        }

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
            }
        }
    }
}
