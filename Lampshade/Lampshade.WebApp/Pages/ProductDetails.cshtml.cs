using _0.Framework.Application;
using Lampshade.Query.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductComment;

namespace Lampshade.WebApp.Pages
{
    public class ProductDetailsModel : PageModel
    {
        #region Constractor Injection

        private readonly IProductQuery _productQuery;
        private readonly IProductCommentApplication _productCommentApplication;
        public ProductDetailsModel(IProductQuery productQuery, IProductCommentApplication productCommentApplication)
        {
            _productQuery = productQuery;
            _productCommentApplication = productCommentApplication;
        }

        #endregion

        public ProductQueryModel Product { get; set; }

        [BindProperty]
        public AddCommentCommand Command { get; set; }

        public void OnGet(string slug)
        {
            Product = _productQuery.GetDetailsBy(slug);
        }

        public IActionResult OnPost(string productSlug)
        {
            var result = _productCommentApplication.Add(Command);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return RedirectToPage("ProductDetails", new { slug = productSlug });
        }
    }
}
