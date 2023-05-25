using _0.Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IGenericRepository<long, ProductPicture>
    {
        EditProductPictureCommand GetDetailForEdit(long id);
        List<ProductPictureViewModel> GetByFilter(ProductPictureSearchModel searchModel);
    }
}
