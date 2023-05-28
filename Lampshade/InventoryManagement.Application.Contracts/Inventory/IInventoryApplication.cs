using _0.Framework.Application;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventoryCommand command);
        OperationResult Edit(EditInventoryCommand command);
        EditInventoryCommand GetForEdit(long id);
        List<InventoryViewModel> GetList(InventorySearchModel searchModel);
        OperationResult Increase(IncreaseInventoryCommand command);
        OperationResult Reduce(List<ReducrInventoryCommand> command);
        OperationResult Reduce(ReducrInventoryCommand command);
    }
}
