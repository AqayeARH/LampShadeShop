namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class EditProductCategoryCommand : CreateProductCategoryCommand
    {
        public long Id { get; set; }
    }
}
