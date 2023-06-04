namespace ShopManagement.Application.Contracts.ProductComment
{
    public class ProductCommentViewModel
    {
        public long ProductId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public string ProductName { get; set; }
        public int Status { get; set; }
    }
}
