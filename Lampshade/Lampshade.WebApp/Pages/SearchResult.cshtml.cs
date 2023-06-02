using Lampshade.Query.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Pages
{
    public class SearchResultModel : PageModel
    {
        #region Constractor Injection

        private readonly IProductQuery _productQuery;
        public SearchResultModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        #endregion

        public List<ProductQueryModel> Products { get; set; }

        public void OnGet(string searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                ViewData["search"] = "همه ی محصولات";
            }
            else
            {
                ViewData["search"] = searchValue;
            }

            Products = _productQuery.Search(searchValue);
        }
    }
}
