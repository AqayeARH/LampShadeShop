using Lampshade.Query.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace Lampshade.WebApp.ViewComponents
{
    public class LatestProductsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;
        public LatestProductsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = _productQuery.GetLatestProducts();
            return View("/Pages/Shared/Components/Product/LatestProducts.cshtml", model);
        }
    }
}
