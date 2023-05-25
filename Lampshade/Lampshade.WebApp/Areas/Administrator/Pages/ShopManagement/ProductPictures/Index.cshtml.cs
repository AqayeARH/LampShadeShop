using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace Lampshade.WebApp.Areas.Administrator.Pages.ShopManagement.ProductPictures
{
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        #endregion
        public List<ProductPictureViewModel> Pictures { get; set; }
        public ProductPictureSearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Pictures = _productPictureApplication.GetList(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        //Handlers
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateProductPictureCommand()
            {
                Products = _productApplication.GetProducts()
            });
        }

        public IActionResult OnPostCreate(CreateProductPictureCommand command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var model = _productPictureApplication.GetForEdit(id);
            model.Products = _productApplication.GetProducts();
            return Partial("Edit", model);
        }

        public IActionResult OnPostEdit(EditProductPictureCommand command)
        {
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            _productPictureApplication.Remove(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            _productPictureApplication.Restore(id);
            return RedirectToPage("Index");
        }
    }
}
