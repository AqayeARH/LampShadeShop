using Lampshade.Query.Common;
using Lampshade.Query.Contracts.ArticleCategory;
using Lampshade.Query.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace Lampshade.WebApp.ViewComponents
{
    public class SiteMenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        public SiteMenuViewComponent(IProductCategoryQuery productCategoryQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = new MenuQueryModel()
            {
                ArticleCategories = _articleCategoryQuery.GetArticleCategoriesInMenu(),
                ProductCategories = _productCategoryQuery.GetProductCategoriesInMenu()
            };
            return View("/Pages/Shared/Components/Menu/SiteMenu.cshtml", model);
        }
    }
}
