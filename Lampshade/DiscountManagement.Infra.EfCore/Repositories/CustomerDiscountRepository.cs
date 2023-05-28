using System.Globalization;
using _0.Framework.Application;
using _0.Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infra.EfCore;

namespace DiscountManagement.Infra.EfCore.Repositories
{
    public class CustomerDiscountRepository : EfCoreGenericRepository<long, CustomerDiscount> , ICustomerDiscountRepository
    {
        #region Constractor Injection

        private readonly DiscountContext _context;
        //Inject Shop Context For Get Name Product
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        #endregion

        public EditCustomerDiscountCommand GetDetailForEdit(long id)
        {
            return _context.CustomerDiscounts
                .Select(x => new EditCustomerDiscountCommand
                {
                    ProductId = x.ProductId,
                    DiscountRate = x.DiscountRate,
                    StartDate = x.StartDate.ToString(CultureInfo.InvariantCulture),
                    EndDate = x.EndDate.ToString(CultureInfo.InvariantCulture),
                    Reason = x.Reason,
                    Id = x.Id
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> GetByFilter(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products
                .Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.CustomerDiscounts
                .AsEnumerable()
                .Select(x => new CustomerDiscountViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    DiscountRate = x.DiscountRate,
                    StartDate = x.StartDate.ToFarsi(),
                    EndDate = x.EndDate.ToFarsi(),
                    Reason = x.Reason,
                    MainEndDate = x.EndDate,
                    MainStartDate = x.StartDate,
                    ProductName = products.FirstOrDefault(p=>p.Id == x.ProductId)?.Name
                }).AsQueryable();

            #region Filtering

            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (!string.IsNullOrEmpty(searchModel.StartDate))
            {
                query = query.Where(x => x.MainStartDate >= searchModel.StartDate.ToGeorgianDateTime());
            }

            if (!string.IsNullOrEmpty(searchModel.EndDate))
            {
                query = query.Where(x => x.MainEndDate <= searchModel.EndDate.ToGeorgianDateTime());
            }

            #endregion

            return query.OrderByDescending(x => x.Id).ToList();

            /*
            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(d=> d.ProductName = products.FirstOrDefault(w=>w.Id == d.ProductId)?.Name);
            return discounts;
            */
        }
    }
}
