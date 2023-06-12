using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace Lampshade.WebApp.Areas.Administrator.Pages.DiscountsManagement.CustomerDiscounts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        #endregion

        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }
        public CustomerDiscountSearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            CustomerDiscounts = _customerDiscountApplication.GetList(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        //Handlers
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new DefineCustomerDiscountCommand()
            {
                Products = _productApplication.GetProducts()
            });
        }

        public IActionResult OnPostCreate(DefineCustomerDiscountCommand command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var model = _customerDiscountApplication.GetForEdit(id);
            model.Products = _productApplication.GetProducts();
            return Partial("Edit", model);
        }

        public IActionResult OnPostEdit(EditCustomerDiscountCommand command)
        {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
