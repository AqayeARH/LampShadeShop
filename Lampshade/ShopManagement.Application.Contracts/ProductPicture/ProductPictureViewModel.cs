namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class ProductPictureViewModel
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Picture { get; set; }
        public long ProductId { get; set; }
        public bool IsRemoved { get; set; }
    }
}
