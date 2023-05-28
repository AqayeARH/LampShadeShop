using _0.Framework.Domain;

namespace DiscountManagement.Domain.ColleaguesDiscountAgg
{
    public class ColleaguesDiscount : BaseEntity
    {
        public long ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public bool IsRemoved { get; private set; }


        //The constructor is called when creating a new instance
        public ColleaguesDiscount(long productId, int discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            IsRemoved = false;
        }

        //The edit method is called when an entity is changed
        public void Edit(long productId, int discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
