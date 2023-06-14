using System.Reflection;
using _0.Framework.Application.Authentication;
using _0.Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lampshade.WebApp
{
    public class SecurityPageFilter : IPageFilter
    {
        #region Constractor Injection

        private readonly IAuthenticationHelper _authenticationHelper;
        public SecurityPageFilter(IAuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }

        #endregion
        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {

        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var permission = (NeedPermissionAttribute)context.HandlerMethod?.MethodInfo.GetCustomAttribute(typeof(NeedPermissionAttribute));

            if (permission != null)
            {
                var accountPermission = _authenticationHelper.GetPermissions();

                if (accountPermission.All(x => x != permission.PermissionCode))
                {
                    context.HttpContext.Response.Redirect("/AccessDenied");
                }
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {

        }
    }
}
