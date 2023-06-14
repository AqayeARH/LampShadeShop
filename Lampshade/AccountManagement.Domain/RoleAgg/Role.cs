using _0.Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : BaseEntity
    {
        public string Name { get; private set; }

        public List<Account> Accounts { get; private set; }
        public List<RolePermission> Permissions { get; private set; }

        //Empty constructor for relations
        protected Role()
        {
            Accounts = new List<Account>();
        }

        //The constructor is called when creating a new instance
        public Role(string name, List<RolePermission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }

        //The edit method is called when an entity is changed
        public void Edit(string name, List<RolePermission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
