using InventoryManagement.Infra.EfCore;
using Lampshade.Query.Contracts.Inventory;
using ShopManagement.Infra.EfCore;

namespace Lampshade.Query.Queries
{
    public class InventoryQuery : IInventoryQuery
    {
        #region Constractor Injection

        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;
        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        #endregion
        public StockStatus CheckStockStatus(IsInStockCommand command)
        {
            var inventory = _inventoryContext.Inventories
                .FirstOrDefault(x => x.ProductId == command.ProductId);

            if (inventory == null || inventory.CalculateCurrentCountInStock() < command.Count)
            {
                var product = _shopContext.Products
                    .Select(x => new { x.Id, x.Name })
                    .FirstOrDefault(x => x.Id == command.ProductId);
                return new StockStatus()
                {
                    IsInStock = false,
                    ProductName = product?.Name
                };
            }

            return new StockStatus()
            {
                IsInStock = true
            };
        }
    }
}
