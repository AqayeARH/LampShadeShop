namespace AccountManagement.Domain.RoleAgg
{
    public class RolePermission
    {
        public int Code { get; private set; }
        public string Name { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }

        //Empty constructor for relations
        protected RolePermission()
        {
            
        }

        //The constructor is called when creating a new instance
        public RolePermission(int code)
        {
            Code = code;
        }

        public RolePermission(int code, string name, long roleId)
        {
            Code = code;
            Name = name;
            RoleId = roleId;
        }
    }
}
