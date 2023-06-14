using _0.Framework.Infrastructure;

namespace AccountManagement.Application.Contracts.Role
{
    public class EditRoleCommand : CreateRoleCommand
    {
        public long Id { get; set; }
        public List<PermissionDto> MappedPermissions { get; set; }
    }
}
