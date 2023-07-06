using Lampshade.Query.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace Lampshade.WebApp.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; }

        public const string CookieName = "cart-items";
        private readonly IProductQuery _productQuery;
        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
            CartItems = new List<CartItem>();
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

                CartItems = _productQuery.CheckInventoryStatus(cartItems);
            }
        }

        public IActionResult OnGetRemoveItemFromCart(long id)
        {

            var serializer = new JavaScriptSerializer();

            //When we want to receive a cookie, we do it from request.
            var value = Request.Cookies[CookieName];

            //When we want to change a cookie, we do it from the response.
            Response.Cookies.Delete(CookieName);

            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);

            if (itemToRemove == null)
            {
                return Page();
            }

            cartItems.Remove(itemToRemove);

            //Now add new items to the cookie.
            var options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(2),
                Path = "/"
            };

            Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);

            return RedirectToPage("/Cart");
        }

        public IActionResult OnGetGoToCheckout()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            if (cartItems != null && cartItems.Any())
            {
                foreach (var item in cartItems)
                {
                    item.TotalItemPrice = item.UnitPrice * item.Count;
                }

                CartItems = _productQuery.CheckInventoryStatus(cartItems);

                if (CartItems.Any(x=>x.IsInStock == false))
                {
                    return Page();
                }

                return RedirectToPage("Checkout");
            }

            return Page();
        }
    }
}
