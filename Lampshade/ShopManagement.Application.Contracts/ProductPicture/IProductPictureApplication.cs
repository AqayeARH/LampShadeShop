using _0.Framework.Application;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPictureCommand command);
        OperationResult Edit(EditProductPictureCommand command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditProductPictureCommand GetForEdit(long id);
        List<ProductPictureViewModel> GetList(ProductPictureSearchModel searchModel);
    }
}
