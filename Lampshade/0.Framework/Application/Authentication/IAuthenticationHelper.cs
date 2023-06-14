namespace _0.Framework.Application.Authentication
{
    public interface IAuthenticationHelper
    {
        void Signin(AuthenticationViewModel account);
        bool IsAuthenticated();
        void SignOut();
        int CurrentAccountRole();
        AuthenticationViewModel CurrentAccountInfo();
        List<int> GetPermissions();
    }
}
