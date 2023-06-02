using Lampshade.Query.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Pages
{
    public class ProductsCategoryModel : PageModel
    {
        #region Constractor Injection

        private readonly IProductCategoryQuery _productCategoryQuery;
        public ProductsCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        #endregion

        public ProductCategoryQueryModel ProductsCategory { get; set; }

        public void OnGet(string slug)
        {
            ProductsCategory = _productCategoryQuery.GetCategoryWithProducts(slug);
        }
    }
}
