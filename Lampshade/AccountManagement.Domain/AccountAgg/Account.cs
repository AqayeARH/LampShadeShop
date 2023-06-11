using _0.Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : BaseEntity
    {
        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public string Profile { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }


        //Empty constructor for relations
        protected Account()
        {
            
        }

        //The constructor is called when creating a new instance
        public Account(string fullname, string username, string password, string mobile, string profile, long roleId)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            Mobile = mobile;
            Profile = profile;
            RoleId = roleId;
        }

        //The edit method is called when an entity is changed
        public void Edit(string fullname, string username, string mobile, string profile, long roleId)
        {
            Fullname = fullname;
            Username = username;
            Mobile = mobile;
            if (!string.IsNullOrEmpty(profile))
            {
                Profile = profile;
            }
            RoleId = roleId;
        }

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}
