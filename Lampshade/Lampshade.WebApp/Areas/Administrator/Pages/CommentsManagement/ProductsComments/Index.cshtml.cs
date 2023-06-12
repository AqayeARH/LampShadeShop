using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace Lampshade.WebApp.Areas.Administrator.Pages.CommentsManagement.ProductsComments
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constractoe Injection

        private readonly ICommentApplication _commentApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(ICommentApplication commentApplication, IProductApplication productApplication)
        {
            _commentApplication = commentApplication;
            _productApplication = productApplication;
        }

        #endregion

        public List<CommentViewModel> ProductComments { get; set; }
        public CommentSearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }

        public void OnGet(CommentSearchModel searchModel)
        {
            ProductComments = _commentApplication.GetProductCommentsList(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        //Handlers
        public IActionResult OnGetConfirm(long id)
        {
            _commentApplication.Confirm(id);

            return RedirectToPage("Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            _commentApplication.Cancel(id);

            return RedirectToPage("Index");
        }
    }
}
