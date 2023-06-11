using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lampshade.WebApp.Areas.Administrator.Pages.AccountsManagement.Accounts
{
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        #endregion

        public List<AccountViewModel> Accounts { get; set; }
        public AccountSearchModel SearchModel { get; set; }
        public SelectList Roles { get; set; }

        public void OnGet(AccountSearchModel searchModel)
        {
            Accounts = _accountApplication.GetList(searchModel);
            Roles = new SelectList(_roleApplication.GetList(), "Id", "Name");
        }

        //Handlers
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateAccountCommand()
            {
                Roles = _roleApplication.GetList()
            });
        }

        public IActionResult OnPostCreate(CreateAccountCommand command)
        {
            var result = _accountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var model = _accountApplication.GetForEdit(id);
            model.Roles = _roleApplication.GetList();
            return Partial("Edit", model);
        }

        public IActionResult OnPostEdit(EditAccountCommand command)
        {
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            return Partial("ChangePassword", new ChangePasswordCommand()
            {
                Id = id
            });
        }

        public IActionResult OnPostChangePassword(ChangePasswordCommand command)
        {
            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }
    }
}
