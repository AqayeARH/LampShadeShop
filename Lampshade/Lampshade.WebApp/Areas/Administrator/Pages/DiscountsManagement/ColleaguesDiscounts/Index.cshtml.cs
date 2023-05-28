using DiscountManagement.Application.Contracts.ColleaguesDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace Lampshade.WebApp.Areas.Administrator.Pages.DiscountsManagement.ColleaguesDiscounts
{
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly IColleaguesDiscountApplication _colleaguesDiscountApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IColleaguesDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _colleaguesDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        #endregion

        public List<ColleaguesDiscountViewModel> ColleaguesDiscounts { get; set; }
        public ColleaguesDiscountSearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }

        public void OnGet(ColleaguesDiscountSearchModel searchModel)
        {
            ColleaguesDiscounts = _colleaguesDiscountApplication.GetList(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        //Handlers
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new DefineColleaguesDiscountCommand()
            {
                Products = _productApplication.GetProducts()
            });
        }

        public IActionResult OnPostCreate(DefineColleaguesDiscountCommand command)
        {
            var result = _colleaguesDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var model = _colleaguesDiscountApplication.GetForEdit(id);
            model.Products = _productApplication.GetProducts();
            return Partial("Edit", model);
        }

        public IActionResult OnPostEdit(EditColleaguesDiscountCommand command)
        {
            var result = _colleaguesDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            _colleaguesDiscountApplication.Remove(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            _colleaguesDiscountApplication.Restore(id);
            return RedirectToPage("Index");
        }
    }
}
