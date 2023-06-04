using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductComment;

namespace Lampshade.WebApp.Areas.Administrator.Pages.ShopManagement.ProductComment
{
    public class IndexModel : PageModel
    {
        #region Constractoe Injection

        private readonly IProductCommentApplication _productCommentApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IProductCommentApplication productCommentApplication, IProductApplication productApplication)
        {
            _productCommentApplication = productCommentApplication;
            _productApplication = productApplication;
        }

        #endregion

        public List<ProductCommentViewModel> ProductComments { get; set; }
        public ProductCommentSearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }

        public void OnGet(ProductCommentSearchModel searchModel)
        {
            ProductComments = _productCommentApplication.GetList(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        //Handlers
        public IActionResult OnGetConfirm(long id)
        {
            _productCommentApplication.Confirm(id);

            return RedirectToPage("Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            _productCommentApplication.Cancel(id);

            return RedirectToPage("Index");
        }
    }
}
