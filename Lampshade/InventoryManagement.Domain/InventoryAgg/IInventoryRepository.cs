using _0.Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IGenericRepository<long, Inventory>
    {
        EditInventoryCommand GetDetailForEdit(long id);
        List<InventoryViewModel> GetByFilter(InventorySearchModel searchModel);
        Inventory GetByProductId(long productId);
    }
}
