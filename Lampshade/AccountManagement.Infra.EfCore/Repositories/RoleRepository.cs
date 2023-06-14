using _0.Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infra.EfCore.Repositories
{
    public class RoleRepository : EfCoreGenericRepository<long, Role>, IRoleRepository
    {
        #region Constractor Injection

        private readonly AccountContext _context;

        public RoleRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public EditRoleCommand GetDetailForEdit(long id)
        {
            return _context.Roles
                .Include(x => x.Permissions)
                .Select(x => new EditRoleCommand()
                {
                    Id = x.Id,
                    Name = x.Name,
                    MappedPermissions = x.Permissions.Select(p => new PermissionDto(p.Code, p.Name)).ToList(),
                }).FirstOrDefault(x => x.Id == id);
        }
    }
}
