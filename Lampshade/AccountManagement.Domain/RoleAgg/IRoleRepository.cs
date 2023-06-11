using _0.Framework.Domain;
using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IGenericRepository<long, Role>
    {
        EditRoleCommand GetDetailForEdit(long id);
    }
}
