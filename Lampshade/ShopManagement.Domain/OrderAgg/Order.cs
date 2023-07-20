using _0.Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : BaseEntity
    {
        public long AccountId { get; private set; }
        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPayed { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public int PaymentMethod { get; private set; }
        public long RefId { get; private set; }

        public List<OrderItem> OrderItems { get; private set; }

        //The constructor is called when creating a new instance
        public Order(long accountId, double totalAmount, double discountAmount, double payAmount, int paymentMethod)
        {
            AccountId = accountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            PaymentMethod = paymentMethod;
            RefId = 0;
            IsPayed = false;
            IsCanceled = false;
            RefId = 0;
            OrderItems = new List<OrderItem>();
		}

        //Empty constructor for relations
        public Order()
        {
            
        }

        public void PaymentSuccess(long refId)
        {
            IsPayed = true;

            if (refId != 0)
            {
                RefId = refId;
            }
        }

        public void SetIssueTrackingNo(string number)
        {
            IssueTrackingNo = number;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void AddItem(OrderItem item)
        {
            OrderItems.Add(item);
        }
    }
}
