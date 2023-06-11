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

        [BindProperty]
        public LoginViewModel LoginModel { get; set; }

        [BindProperty]
        public RegisterAccountCommand RegisterModel { get; set; }

        public void OnGet()
        {
           
        }

        public IActionResult OnPostLogin()
        {
            var result = _accountApplication.Login(LoginModel);

            if (result.IsSuccess)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("LoginModel.Username", result.Message);
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("Index");
        }

        public IActionResult OnPostRegister()
        {
            if (!RegisterModel.Password.Equals(RegisterModel.RePassword))
            {
                ModelState.AddModelError("RegisterModel.RePassword", "ò·„Â ⁄»Ê— »«  ò—«— ¬‰ „ÿ«»ﬁ  ‰œ«—œ");
                return Page();
            }

            var result = _accountApplication.Register(RegisterModel);

            if (result.IsSuccess)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("RegisterModel.Username", result.Message);
            return Page();
        }
    }
}
