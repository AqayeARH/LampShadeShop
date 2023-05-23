using _0.Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IGenericRepository<long, ProductCategory>
    {
        EditProductCategoryCommand GetDetailForEdit(long id);
        List<ProductCategoryViewModel> GetByFilter(ProductCategorySearchModel searchModel);
        List<ProductCategoryViewModel> GetProductCategories();
    }
}
