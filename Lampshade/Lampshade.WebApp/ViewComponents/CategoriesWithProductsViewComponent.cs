using Lampshade.Query.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace Lampshade.WebApp.ViewComponents
{
    public class CategoriesWithProductsViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        public CategoriesWithProductsViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = _productCategoryQuery.GetCategoriesWithProductsInIndex();
            return View("/Pages/Shared/Components/ProductCategory/CategoriesWithProducts.cshtml", model);
        }
    }
}
