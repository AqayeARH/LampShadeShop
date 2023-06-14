namespace _0.Framework.Application.Authentication
{
    public class AuthenticationViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public bool RememberMe { get; set; }
        public string RoleName { get; set; }
        public List<int> Permissions { get; set; }
    }
}
