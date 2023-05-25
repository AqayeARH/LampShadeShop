namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class EditProductPictureCommand : CreateProductPictureCommand
    {
        public long Id { get; set; }
    }
}
