using _0.Framework.Application;
using _0.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductComment;
using ShopManagement.Domain.ProductCommentAgg;

namespace ShopManagement.Infra.EfCore.Repositories
{
    public class ProductCommentRepository : EfCoreGenericRepository<long, ProductComment>, IProductCommentRepository
    {
        #region Constractor Injection

        private readonly ShopContext _context;
        public ProductCommentRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion
        public List<ProductCommentViewModel> GetByFilter(ProductCommentSearchModel searchModel)
        {
            var query = _context.ProductComments
                .Include(x => x.Product)
                .Select(x => new ProductCommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Text = x.Text,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ProductName = x.Product.Name,
                    Status = x.Status,
                    ProductId = x.ProductId
                }).AsQueryable();

            #region Filtering

            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            #endregion

            return query.OrderByDescending(x => x.Id).ToList();

        }
    }
}
