using Lampshade.Query.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace Lampshade.WebApp.ViewComponents
{
    public class ProductCategoriesViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        public ProductCategoriesViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productCategories = _productCategoryQuery.ShowCategoriesInIndex();
            return View("/Pages/Shared/Components/ProductCategory/ProductCategories.cshtml",productCategories);
        }
    }
}
