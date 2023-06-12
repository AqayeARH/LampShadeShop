using System.Security.Claims;
using _0.Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace _0.Framework.Application.Authentication
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthenticationHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public AuthenticationViewModel CurrentAccountInfo()
        {
            var result = new AuthenticationViewModel();
            if (!IsAuthenticated())
            {
                return result;
            }

            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            result.Id = long.Parse(claims.First(x => x.Type == "AccountId").Value);
            result.Username = claims.First(x => x.Type == "Username").Value;
            result.RoleId = long.Parse(claims.First(x => x.Type == ClaimTypes.Role).Value);
            result.Fullname = claims.First(x => x.Type == ClaimTypes.Name).Value;
            result.RoleName = Roles.GetRoleNameBy(result.RoleId);
            return result;
        }

        //public List<int> GetPermissions()
        //{
        //    if (!IsAuthenticated())
        //        return new List<int>();

        //    var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "permissions")
        //        ?.Value;
        //    //return JsonConvert.DeserializeObject<List<int>>(permissions);
        //}

        //public long CurrentAccountId()
        //{
        //    return IsAuthenticated()
        //        ? long.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountId")?.Value)
        //        : 0;
        //}

        //public string CurrentAccountMobile()
        //{
        //    return IsAuthenticated()
        //        ? _contextAccessor.HttpContext.User.Claims.First(x => x.Type == "Mobile")?.Value
        //        : "";
        //}

        public int CurrentAccountRole()
        {
            return IsAuthenticated() ? int.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value) : 0;
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity is { IsAuthenticated: true };
        }

        public void Signin(AuthenticationViewModel account)
        {
            //var permissions = JsonConvert.SerializeObject(account.Permissions);
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("Username", account.Username),
                //new Claim("permissions", permissions),
                //new Claim("Mobile", account.Mobile)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IsPersistent = account.RememberMe
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}