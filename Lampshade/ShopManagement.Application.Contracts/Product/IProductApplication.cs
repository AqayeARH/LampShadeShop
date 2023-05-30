using _0.Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProductCommand command);
        OperationResult Edit(EditProductCommand command);
        EditProductCommand GetForEdit(long id);
        List<ProductViewModel> GetList(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();
        //OperationResult InStock(long id);
        //OperationResult NotInStock(long id);
    }
}
