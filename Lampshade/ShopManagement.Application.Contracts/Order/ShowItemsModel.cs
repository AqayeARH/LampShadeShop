namespace ShopManagement.Application.Contracts.Order
{
    public class ShowItemsModel
    {
        public List<OrderItemViewModel> OrderItems { get; set; }
        public string OrderId { get; set; }
    }
}
