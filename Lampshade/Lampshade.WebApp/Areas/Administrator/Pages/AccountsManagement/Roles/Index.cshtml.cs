using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Areas.Administrator.Pages.AccountsManagement.Roles
{
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly IRoleApplication _roleApplication;
        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        #endregion

        public List<RoleViewModel> Roles { get; set; }

        public void OnGet()
        {
            Roles = _roleApplication.GetList();
        }

        //Handlers
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateRoleCommand());
        }

        public IActionResult OnPostCreate(CreateRoleCommand command)
        {
            var result = _roleApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var model = _roleApplication.GetForEdit(id);
            return Partial("Edit", model);
        }

        public IActionResult OnPostEdit(EditRoleCommand command)
        {
            var result = _roleApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
