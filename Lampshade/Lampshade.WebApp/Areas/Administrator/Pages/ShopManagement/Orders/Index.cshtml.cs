using _0.Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Infra.Configuration.Permissions;

namespace Lampshade.WebApp.Areas.Administrator.Pages.ShopManagement.Orders
{
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;
        public IndexModel(IOrderApplication orderApplication, IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;
        }

        #endregion

        public List<OrderViewModel> Orders { get; set; }
        public OrderSearchModel SearchModel { get; set; }
        public SelectList Accounts { get; set; }

        [NeedPermission(ShopPermissionCode.ListOrders)]
        public void OnGet(OrderSearchModel searchModel)
        {
            Orders = _orderApplication.GetList(searchModel);
            Accounts = new SelectList(_accountApplication.GetAccounts(),
                "Id", "Fullname");
        }

        public IActionResult OnGetConfirm(long id)
        {
            _orderApplication.PaymentSuccess(id, 0);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            _orderApplication.Cancel(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetShowItems(long id)
        {
            ViewData["OrderId"] = id;
            var model = new ShowItemsModel()
            {
                OrderId = id.ToString(),
                OrderItems = _orderApplication.GetListItems(id)
            };

            return Partial("OrderItems", model);
        }
    }
}
