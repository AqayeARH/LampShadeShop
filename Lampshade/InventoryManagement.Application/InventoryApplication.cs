using _0.Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        #region Constracto Injection

        private readonly IInventoryRepository _inventoryRepository;
        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        #endregion
        public OperationResult Create(CreateInventoryCommand command)
        {
            var operation = new OperationResult();

            if (_inventoryRepository.IsExist(x => x.ProductId == command.ProductId))
            {
                return operation.Error("برای این محصول قبلا سیستم انبار داری تعریف شده است");
            }

            var inventory = new Inventory(command.ProductId, command.UnitPrice);

            _inventoryRepository.Create(inventory);
            _inventoryRepository.Save();
            return operation.Success();
        }

        public OperationResult Edit(EditInventoryCommand command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.Id);

            if (inventory == null)
            {
                return operation.Error("انباری در سیستم انبارداری با مشخصات ارسالی یافت نشد");
            }

            if (_inventoryRepository.IsExist(x => x.ProductId == command.ProductId && x.Id != command.Id))
            {
                return operation.Error("برای این محصول قبلا سیستم انبار داری تعریف شده است");
            }

            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.Save();
            return operation.Success();
        }

        public EditInventoryCommand GetForEdit(long id)
        {
            return _inventoryRepository.GetDetailForEdit(id);
        }

        public List<InventoryViewModel> GetList(InventorySearchModel searchModel)
        {
            return _inventoryRepository.GetByFilter(searchModel);
        }

        public OperationResult Increase(IncreaseInventoryCommand command)
        {
            var operation = new OperationResult();

            var inventory = _inventoryRepository.GetBy(command.InventoryId);

            if (inventory == null)
            {
                return operation.Error("انباری در سیستم انبارداری با مشخصات ارسالی یافت نشد");
            }

            const long operatorId = 1;

            inventory.Increase(command.Count, operatorId, command.Description);
            _inventoryRepository.Save();

            return operation.Success();
        }

        public OperationResult Reduce(List<ReduceInventoryCommand> command)
        {
            var operation = new OperationResult();

            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);

                if (inventory == null)
                {
                    return operation.Error();
                }

                const long operatorId = 1;

                inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
            }

            _inventoryRepository.Save();

            return operation.Success();
        }

        public OperationResult Reduce(ReduceInventoryCommand command)
        {
            var operation = new OperationResult();

            var inventory = _inventoryRepository.GetBy(command.InventoryId);

            if (inventory == null)
            {
                return operation.Error("انباری در سیستم انبارداری با مشخصات ارسالی یافت نشد");
            }

            const long operatorId = 1;
            const long orderId = 0;

            inventory.Reduce(command.Count, operatorId, command.Description, orderId);
            _inventoryRepository.Save();

            return operation.Success();
        }

        public List<InventoryOperationViewModel> GetOperationsLog(long inventoryId)
        {
            return _inventoryRepository.GetInventoryOperations(inventoryId);
        }
    }
}