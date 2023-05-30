namespace ShopManagement.Application.Contracts.Product
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public string Picture { get; set; }
        public long CategoryId { get; set; }
        public string CreationDate { get; set; }
        //public string UnitPrice { get; set; }
        //public bool IsInStock { get; set; }
    }
}
