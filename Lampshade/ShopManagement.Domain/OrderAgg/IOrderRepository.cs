using _0.Framework.Domain;
using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IGenericRepository<long, Order>
    {
	    double GetAmountBy(long id);
        List<OrderViewModel> GetByFilter(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetOrderItems(long orderId);
    }
}
