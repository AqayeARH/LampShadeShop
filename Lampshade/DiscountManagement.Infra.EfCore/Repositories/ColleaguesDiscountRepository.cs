using _0.Framework.Application;
using _0.Framework.Infrastructure;
using DiscountManagement.Application.Contracts.ColleaguesDiscount;
using DiscountManagement.Domain.ColleaguesDiscountAgg;
using ShopManagement.Infra.EfCore;

namespace DiscountManagement.Infra.EfCore.Repositories
{
    public class ColleaguesDiscountRepository : EfCoreGenericRepository<long, ColleaguesDiscount>, IColleaguesDiscountRepository
    {
        #region Constractor Injection

        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;
        public ColleaguesDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        #endregion
        public EditColleaguesDiscountCommand GetDetailForEdit(long id)
        {
            return _context.ColleaguesDiscounts
                .Select(x => new EditColleaguesDiscountCommand
                {
                    ProductId = x.ProductId,
                    DiscountRate = x.DiscountRate,
                    Id = x.Id
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ColleaguesDiscountViewModel> GetByFilter(ColleaguesDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products
                .Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.ColleaguesDiscounts
                .AsEnumerable()
                .Select(x => new ColleaguesDiscountViewModel
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ProductId = x.ProductId,
                    DiscountRate = x.DiscountRate,
                    ProductName = products.FirstOrDefault(p=>p.Id==x.ProductId)?.Name,
                    IsRemoved = x.IsRemoved
                }).AsQueryable();

            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            return query.ToList();
        }
    }
}
