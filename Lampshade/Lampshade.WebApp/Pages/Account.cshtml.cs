using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Pages
{
    [ValidateAntiForgeryToken]
    public class AccountModel : PageModel
    {
        #region Constractor Injection

        private readonly IAccountApplication _accountApplication;
        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        #endregion

        public void OnGet()
        {
           
        }

        public IActionResult OnPostLogin(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = _accountApplication.Login(loginModel);

            if (result.IsSuccess)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("Username", result.Message);
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("Index");
        }

        public IActionResult OnPostRegister(RegisterAccountCommand registerModel)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!registerModel.Password.Equals(registerModel.RePassword))
            {
                ModelState.AddModelError("RePassword", "ò·„Â ⁄»Ê— »«  ò—«— ¬‰ „ÿ«»ﬁ  ‰œ«—œ");
                return Page();
            }

            var result = _accountApplication.Register(registerModel);

            if (result.IsSuccess)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("Username", result.Message);
            return Page();
        }
    }
}
