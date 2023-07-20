using InventoryManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infra.InventoryAcl
{
    public class ShopInventoryAcl: IShopInventoryAcl
    {
        #region Constractor Injection

        private readonly IInventoryApplication _inventoryApplication;
        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        #endregion
        public bool ReduceFromInventory(List<OrderItem> orderItems)
        {
            var command = new List<ReduceInventoryCommand>();

            foreach (var item in orderItems)
            {
                var reduce = new ReduceInventoryCommand()
                {
                    Count = item.Count,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Description = $"کسر از فاکتور {item.OrderId} توسط مشتری"
                };
                command.Add(reduce);
            }

            var result = _inventoryApplication.Reduce(command);

            return result.IsSuccess;
        }
    }
}