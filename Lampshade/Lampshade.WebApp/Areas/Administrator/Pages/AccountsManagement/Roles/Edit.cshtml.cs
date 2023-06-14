using _0.Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lampshade.WebApp.Areas.Administrator.Pages.AccountsManagement.Roles
{
    public class EditModel : PageModel
    {
        #region Constractor Injection

        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposure> _permissionExposures;
        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposure> permissionExposures)
        {
            _roleApplication = roleApplication;
            _permissionExposures = permissionExposures;
        }

        #endregion

        [BindProperty]
        public EditRoleCommand Command { get; set; }

        public List<SelectListItem> Permissions { get; set; }

        public void OnGet(long id)
        {
            Command = _roleApplication.GetForEdit(id);

            Permissions = new List<SelectListItem>();

            var permissions = new List<PermissionDto>();
            foreach (var exposure in _permissionExposures)
            {
                var exposPermission = exposure.Expose();
                foreach (var (key, value) in exposPermission)
                {
                    permissions.AddRange(value);

                    var groupItem = new SelectListGroup()
                    {
                        Name = key
                    };

                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name,permission.Code.ToString())
                        {
                            Group = groupItem
                        };

                        if (Command.MappedPermissions.Any(x=>x.Code == permission.Code))
                        {
                            item.Selected = true;
                        }

                        Permissions.Add(item);
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = _roleApplication.Edit(Command);

            if (result.IsSuccess)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("Command.Name", result.Message);
            return Page();
        }
    }
}
