using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace Lampshade.WebApp.Areas.Administrator.Pages.ShopManagement.ProductCategories
{
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly IProductCategoryApplication _productCategoryApplication;
        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        #endregion

        public List<ProductCategoryViewModel> ProductCategories { get; set; }
        public ProductCategorySearchModel SearchModel { get; set; }
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.GetList(searchModel);
        }


        //Handlers
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateProductCategoryCommand());
        }

        public IActionResult OnPostCreate(CreateProductCategoryCommand command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var model = _productCategoryApplication.GetForEdit(id);
            return Partial("Edit", model);
        }

        public IActionResult OnPostEdit(EditProductCategoryCommand command)
        {
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
