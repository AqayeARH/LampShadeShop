namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class ProductCategoryVewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string CreationDate { get; set; }
        public int ProductsCount { get; set; }
    }
}
