using _0.Framework.Application;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        string PaymentSuccess(long orderId,long refId);
        double GetAmountBy(long id);
        List<OrderViewModel> GetList(OrderSearchModel searchModel);
        OperationResult Cancel(long id);
        List<OrderItemViewModel> GetListItems(long orderId);
    }
}
