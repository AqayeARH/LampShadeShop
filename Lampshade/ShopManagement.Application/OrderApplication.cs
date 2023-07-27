using _0.Framework.Application;
using _0.Framework.Application.Authentication;
using _0.Framework.Application.Sms;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        #region Constractor Injection

        private readonly IOrderRepository _orderRepository;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IConfiguration _configuration;
        private readonly IShopInventoryAcl _shopInventoryAcl;
        private readonly ISmsService _smsService;
        private readonly IShopAccountAcl _shopAccountAcl;
        public OrderApplication(IOrderRepository orderRepository,
            IAuthenticationHelper authenticationHelper, IConfiguration configuration,
            IShopInventoryAcl shopInventoryAcl, ISmsService smsService, IShopAccountAcl shopAccountAcl)
        {
            _orderRepository = orderRepository;
            _authenticationHelper = authenticationHelper;
            _configuration = configuration;
            _shopInventoryAcl = shopInventoryAcl;
            _smsService = smsService;
            _shopAccountAcl = shopAccountAcl;
        }

        #endregion

        public long PlaceOrder(Cart cart)
        {
            var accountId = _authenticationHelper.CurrentAccountId();

            var order = new Order(accountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount,
                cart.PaymentMethod);

            foreach (var item in cart.CartItems)
            {
                //Tip : //Because we put own type, there is no need for order ID
                var orderItem = new OrderItem(item.Id, item.Count, item.UnitPrice, item.DiscountRate);
                order.AddItem(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.Save();
            return order.Id;
        }

        public string PaymentSuccess(long orderId, long refId)
        {
            var order = _orderRepository.GetBy(orderId);
            order.PaymentSuccess(refId);
            var issueTrackingNo = CodeGenerator.Generate(_configuration.GetSection("Symbol").Value);

            order.SetIssueTrackingNo(issueTrackingNo);

            #region Reduce From Inventory

            if (_shopInventoryAcl.ReduceFromInventory(order.OrderItems))
            {
                _orderRepository.Save();
            }

            #endregion

            var (_, name, mobile) = _shopAccountAcl.GetAccountBy(order.AccountId);

            /*
            var message =
                $"{name} گرامی! پرداخت فاکتور به شماره {orderId} با موفقیت انجام شد. شماره پیگیری پرداخت : {issueTrackingNo}";
           */

            _smsService.SendConfirmOrder(mobile, name, issueTrackingNo, orderId.ToString());
            return issueTrackingNo;
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }

        public List<OrderViewModel> GetList(OrderSearchModel searchModel)
        {
            return _orderRepository.GetByFilter(searchModel);
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();

            var order = _orderRepository.GetBy(id);

            if (order == null)
            {
                return operation.Error("فاکتوری با مشخضات ارسالی یافت نشد");
            }

            order.Cancel();
            _orderRepository.Save();

            return operation.Success();
        }

        public List<OrderItemViewModel> GetListItems(long orderId)
        {
            return _orderRepository.GetOrderItems(orderId);
        }
    }
}
