using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Areas.Administrator.Pages.AccountsManagement.Roles
{
    public class CreateModel : PageModel
    {
        #region Constractor Injection

        private readonly IRoleApplication _roleApplication;
        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        #endregion

        [BindProperty]
        public CreateRoleCommand Command { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = _roleApplication.Create(Command);

            if (result.IsSuccess)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("Command.Name", result.Message);
            return Page();
        }
    }
}
