using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using Lampshade.Query.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Pages
{
    public class ProductDetailsModel : PageModel
    {
        #region Constractor Injection

        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductDetailsModel(IProductQuery productQuery, ICommentApplication productCommentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = productCommentApplication;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Command.Type = CommentTypes.Product;
            Command.ParentId = 0;
            _commentApplication.Add(Command);

            return RedirectToPage("ProductDetails", new { slug = productSlug });
        }
    }
}
