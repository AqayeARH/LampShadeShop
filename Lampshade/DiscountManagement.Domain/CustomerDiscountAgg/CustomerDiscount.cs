using _0.Framework.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount : BaseEntity
    {
        public long ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }


        //The constructor is called when creating a new instance
        public CustomerDiscount(long productId, int discountRate, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }

        //The edit method is called when an entity is changed
        public void Edit(long productId, int discountRate, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }
    }
}
