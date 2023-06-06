using _0.Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductCommentAgg
{
    public class ProductComment : BaseEntity
    {
        public long ProductId { get; private set; }
        public Product Product { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Text { get; private set; }
        public int Status { get; private set; }

        //Empty constructor for ef core relations
        protected ProductComment()
        {
            
        }

        //The constructor is called when creating a new instance
        public ProductComment(long productId, string name, string email, string text)
        {
            ProductId = productId;
            Name = name;
            Email = email;
            Text = text;
            Status = ProductCommentStatuses.New;
        }

        public void Confirm()
        {
            Status = ProductCommentStatuses.Confirmed;
        }

        public void Cancel()
        {
            Status = ProductCommentStatuses.Canceled;
        }
    }
}
