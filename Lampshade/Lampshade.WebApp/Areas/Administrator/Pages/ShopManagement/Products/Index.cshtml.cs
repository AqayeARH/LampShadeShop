using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace Lampshade.WebApp.Areas.Administrator.Pages.ShopManagement.Products
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        #endregion

        public List<ProductViewModel> Products { get; set; }
        public ProductSearchModel SearchModel { get; set; }
        public SelectList ProductCategories { get; set; }

        public void OnGet(ProductSearchModel searchModel)
        {
            Products = _productApplication.GetList(searchModel);
            var productCategories = _productCategoryApplication.GetProductCategories();
            ProductCategories = new SelectList(productCategories, "Id", "Name");
        }

        //Handlers
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateProductCommand()
            {
                ProductCategories = _productCategoryApplication.GetProductCategories()
            });
        }

        public IActionResult OnPostCreate(CreateProductCommand command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var model = _productApplication.GetForEdit(id);
            model.ProductCategories = _productCategoryApplication.GetProductCategories();
            return Partial("Edit", model);
        }

        public IActionResult OnPostEdit(EditProductCommand command)
        {
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
