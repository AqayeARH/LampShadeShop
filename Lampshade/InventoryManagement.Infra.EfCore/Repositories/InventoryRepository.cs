using _0.Framework.Application;
using _0.Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infra.EfCore;

namespace InventoryManagement.Infra.EfCore.Repositories
{
    public class InventoryRepository : EfCoreGenericRepository<long, Inventory>, IInventoryRepository
    {
        #region Constractor Injection

        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        #endregion
        public EditInventoryCommand GetDetailForEdit(long id)
        {
            return _context.Inventories
                .Select(x => new EditInventoryCommand
                {
                    ProductId = x.ProductId,
                    UnitPrice = x.UnitPrice,
                    Id = x.Id
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewModel> GetByFilter(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.Inventories
                .AsEnumerable()
                .Select(x => new InventoryViewModel()
                {
                    Id = x.Id,
                    InStock = x.InStock,
                    ProductId = x.ProductId,
                    UnitPrice = x.UnitPrice,
                    ProductName = products.FirstOrDefault(p => p.Id == x.ProductId)?.Name,
                    CurrentCount = x.CalculateCurrentCountInStock()
                }).AsQueryable();

            #region Filtering

            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (searchModel.NotInStock)
            {
                query = query.Where(x => x.InStock == false);
            }

            #endregion

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public Inventory GetByProductId(long productId)
        {
            return _context.Inventories.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<InventoryOperationViewModel> GetInventoryOperations(long inventoryId)
        {
            //First solution
            /* 
            return _context.InventoryOperations
                .Where(x => x.InventoryId == inventoryId)
                .Select(x => new InventoryOperationViewModel()
                {
                    Count = x.Count,
                    CurrentCount = x.CurrentCount,
                    Description = x.Description,
                    Id = x.Id,
                    InventoryId = x.InventoryId,
                    Operation = (x.Operation == true) ? "افزایش موجودی" : "کاهش موجودی",
                    OperationDate = x.OperationDate.ToFarsiFull(),
                    OrderId = x.OrderId,
                    OperatorId = x.OperatorId,
                    OperatorName = "ادمین"
                }).ToList();
            */

            //Second solution
            var inventory = _context.Inventories.FirstOrDefault(x => x.Id == inventoryId);

            if (inventory == null)
            {
                return null;
            }

            return inventory.InventoryOperations
                .OrderByDescending(x => x.Id)
                .Select(x => new InventoryOperationViewModel()
                {
                    Count = x.Count,
                    CurrentCount = x.CurrentCount,
                    Description = x.Description,
                    Id = x.Id,
                    InventoryId = x.InventoryId,
                    OperationType = (x.Operation == true) ? "افزایش موجودی" : "کاهش موجودی",
                    OperationDate = x.OperationDate.ToFarsiFull(),
                    OrderId = x.OrderId,
                    OperatorId = x.OperatorId,
                    OperatorName = "ادمین",
                    Operation = x.Operation
                }).ToList();
        }
    }
}
