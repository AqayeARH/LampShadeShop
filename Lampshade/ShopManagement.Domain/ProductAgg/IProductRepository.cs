using _0.Framework.Domain;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IGenericRepository<long, Product>
    {
        List<ProductViewModel> GetByFilter(ProductSearchModel searchModel);
        EditProductCommand GetDetailForEdit(long id);
    }
}
