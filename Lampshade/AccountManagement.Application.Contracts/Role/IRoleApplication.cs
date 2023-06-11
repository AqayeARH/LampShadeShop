using _0.Framework.Application;

namespace AccountManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRoleCommand command);
        OperationResult Edit(EditRoleCommand command);
        EditRoleCommand GetForEdit(long id);
        List<RoleViewModel> GetList();
    }
}
