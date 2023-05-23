namespace ShopManagement.Application.Contracts.Product
{
    public class EditProductCommand : CreateProductCommand
    {
        public long Id { get; set; }
    }
}
