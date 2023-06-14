using _0.Framework.Application.Authentication;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lampshade.WebApp
{
    [HtmlTargetElement(Attributes = "Permission")]
    public class PermissionTagHelper : TagHelper
    {
        #region Constractor Injection

        private readonly IAuthenticationHelper _authenticationHelper;
        public PermissionTagHelper(IAuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }

        #endregion
        public int Permission { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!_authenticationHelper.IsAuthenticated())
            {
                output.SuppressOutput();
                return;
            }

            var permission = _authenticationHelper.GetPermissions();

            if (permission.All(x => x != Permission))
            {
                output.SuppressOutput();
                return;
            }
            
            base.Process(context, output);
        }
    }
}
