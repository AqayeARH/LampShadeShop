using _0.Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
	public class OrderItem : BaseEntity
	{
		public long OrderId { get; private set; }
		public Order Order { get; private set; }
		public long ProductId { get; private set; }
		public int Count { get; private set; }
		public double UnitPrice { get; private set; }
		public int DiscountRate { get; private set; }

		public OrderItem(long productId, int count, double unitPrice, int discountRate)
		{
			ProductId = productId;
			Count = count;
			UnitPrice = unitPrice;
			DiscountRate = discountRate;
		}
	}
}
