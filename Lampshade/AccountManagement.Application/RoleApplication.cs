using _0.Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        #region Constractor Injection

        private readonly IRoleRepository _roleRepository;
        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        #endregion
        public OperationResult Create(CreateRoleCommand command)
        {
            var operation = new OperationResult();
            if (_roleRepository.IsExist(x=>x.Name == command.Name))
            {
                return operation.Error("امکان ثبت نقش تکراری وجود ندارد");
            }

            var role = new Role(command.Name, new List<RolePermission>());
            _roleRepository.Create(role);
            _roleRepository.Save();

            return operation.Success();
        }

        public OperationResult Edit(EditRoleCommand command)
        {
            var operation = new OperationResult();
            var role = _roleRepository.GetBy(command.Id);
            
            if (role == null)
            {
                return operation.Error("نقشی با مشخصات ارسالی یافت نشد");
            }

            if (_roleRepository.IsExist(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Error("امکان ثبت نقش تکراری وجود ندارد");
            }

            var permissions = new List<RolePermission>();

            foreach (var permissionCode in command.Permissions)
            {
                permissions.Add(new RolePermission(permissionCode, "Permission", command.Id));
            }

            role.Edit(command.Name, permissions);
            _roleRepository.Save();
            return operation.Success();
        }

        public EditRoleCommand GetForEdit(long id)
        {
            return _roleRepository.GetDetailForEdit(id);
        }

        public List<RoleViewModel> GetList()
        {
            var roles = _roleRepository.GetAll()
                .Select(x => new RoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).AsQueryable();

            return roles.OrderByDescending(x => x.Id).ToList();
        }
    }
}
