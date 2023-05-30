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
        OperationResult Reduce(List<ReduceInventoryCommand> command);
        OperationResult Reduce(ReduceInventoryCommand command);
        List<InventoryOperationViewModel> GetOperationsLog(long inventoryId);
    }
}
