using _0.Framework.Application;
using _0.Framework.Infrastructure;
using AccountManagement.Infra.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infra.EfCore.Repositories
{
    public class OrderRepository : EfCoreGenericRepository<long, Order>, IOrderRepository
    {
        #region Constractor Injection

        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;
        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        #endregion

        public double GetAmountBy(long id)
        {
            var order = _context.Orders
                .Select(x => new { x.Id, x.PayAmount })
                .FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                return 0;
            }

            return order.PayAmount;
        }

        public List<OrderViewModel> GetByFilter(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts
                .Select(x => new { x.Id, x.Fullname })
                .ToList();

            var query = _context.Orders
                .Select(x => new OrderViewModel()
                {
                    AccountId = x.AccountId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    DiscountAmount = x.DiscountAmount,
                    Id = x.Id,
                    PayAmount = x.PayAmount,
                    RefId = x.RefId,
                    PaymentMethod = x.PaymentMethod,
                    TotalAmount = x.TotalAmount,
                    IsPayed = x.IsPayed,
                    IsCanceled = x.IsCanceled,
                    IssueTrackingNo = x.IssueTrackingNo ?? "بدون شماره پیگیری",
                }).AsQueryable();

            #region Filtering

            if (searchModel.AccountId > 0)
            {
                query = query.Where(x => x.AccountId == searchModel.AccountId);
            }

            if (searchModel.IsCanceled)
            {
                query = query.Where(x => x.IsCanceled);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.IssueTrackingNo))
            {
                query = query.Where(x => x.IssueTrackingNo.Contains(searchModel.IssueTrackingNo));
            }

            #endregion

            var result = query.OrderByDescending(x => x.Id).ToList();

            foreach (var order in result)
            {
                /*
                switch (order.PaymentMethod)
                {
                    case PaymentMethod.OnlinePayment:
                        order.PaymentMethodName = "پرداخت آنلاین";
                        break;
                    case PaymentMethod.CashPayment:
                        order.PaymentMethodName = "پرداخت نقدی";
                        break;
                }
                */

                order.AccountName = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.Fullname;
                order.PaymentMethodName = Application.Contracts.PaymentMethod.GetPaymentMethodBy(order.PaymentMethod).Name;
            }

            return result;
        }

        public List<OrderItemViewModel> GetOrderItems(long orderId)
        {
            var products = _context.Products
                .Select(x => new { x.Id, x.Name }).ToList();


            var order = _context.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefault(x => x.Id == orderId);

            if (order == null)
            {
                return new List<OrderItemViewModel>();
            }

            var orderItems = order.OrderItems
                .Select(x => new OrderItemViewModel()
                {
                    UnitPrice = x.UnitPrice,
                    Count = x.Count,
                    DiscountRate = x.DiscountRate,
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    Id = x.Id
                }).ToList();

            /*
            var query = _context.OrderItems
                .Where(x => x.OrderId == orderId)
                .Select(x => new OrderItemViewModel()
                {
                    UnitPrice = x.UnitPrice,
                    Count = x.Count,
                    DiscountRate = x.DiscountRate,
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    Id = x.Id
                }).AsQueryable();
          

            var result = query.OrderByDescending(x => x.Id).ToList();
            */

            foreach (var item in orderItems)
            {
                item.ProductName = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }

            return orderItems;
        }
    }
}
