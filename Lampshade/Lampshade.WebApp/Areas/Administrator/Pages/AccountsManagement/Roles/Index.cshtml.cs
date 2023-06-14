using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Areas.Administrator.Pages.AccountsManagement.Roles
{
    [Authorize]
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
    }
}
