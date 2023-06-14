namespace _0.Framework.Infrastructure
{
    public class NeedPermissionAttribute : Attribute
    {
        public int PermissionCode { get; set; }

        public NeedPermissionAttribute(int permissionCode)
        {
            PermissionCode = permissionCode;
        }
    }
}
