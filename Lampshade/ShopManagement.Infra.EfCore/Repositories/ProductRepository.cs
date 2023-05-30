using _0.Framework.Application;
using _0.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infra.EfCore.Repositories
{
    public class ProductRepository : EfCoreGenericRepository<long, Product>, IProductRepository
    {
        #region Constractor Injection

        private readonly ShopContext _context;
        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public List<ProductViewModel> GetByFilter(ProductSearchModel searchModel)
        {
            var query = _context.Products
                .Include(x => x.Category)
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Code = x.Code,
                    Id = x.Id,
                    CategoryName = x.Category.Name,
                    Picture = x.Picture,
                    CategoryId = x.CategoryId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    //UnitPrice = x.UnitPrice.ToString("#,0"),
                    //IsInStock = x.IsInStock
                }).AsQueryable();

            #region Filtering

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Code))
            {
                query = query.Where(x => x.Code.Contains(searchModel.Code));
            }
            if (searchModel.CategoryId != 0)
            {
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            }

            #endregion

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products
                .Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }

        public EditProductCommand GetDetailForEdit(long id)
        {
            return _context.Products
                .Select(x => new EditProductCommand
                {
                    Name = x.Name,
                    CategoryId = x.CategoryId,
                    Code = x.Code,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    KayWords = x.KayWords,
                    MetaDescription = x.MetaDescription,
                    Id = x.Id,
                    //UnitPrice = x.UnitPrice,
                }).FirstOrDefault(x => x.Id == id);
        }
    }
}
