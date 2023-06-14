using _0.Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Infra.Configuration.Permissions;

namespace Lampshade.WebApp.Areas.Administrator.Pages.ShopManagement.ProductCategories
{
    [Authorize]
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

        [NeedPermission(ShopPermissionCode.ListProductCategories)]
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.GetList(searchModel);
        }


        //Handlers

        [NeedPermission(ShopPermissionCode.CreateProductCategory)]
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateProductCategoryCommand());
        }

        [NeedPermission(ShopPermissionCode.CreateProductCategory)]
        public IActionResult OnPostCreate(CreateProductCategoryCommand command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        [NeedPermission(ShopPermissionCode.EditProductCategory)]
        public IActionResult OnGetEdit(long id)
        {
            var model = _productCategoryApplication.GetForEdit(id);
            return Partial("Edit", model);
        }

        [NeedPermission(ShopPermissionCode.EditProductCategory)]
        public IActionResult OnPostEdit(EditProductCategoryCommand command)
        {
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
