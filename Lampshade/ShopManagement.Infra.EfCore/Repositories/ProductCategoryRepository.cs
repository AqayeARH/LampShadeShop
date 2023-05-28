using _0.Framework.Application;
using _0.Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infra.EfCore.Repositories
{
    public class ProductCategoryRepository : EfCoreGenericRepository<long, ProductCategory>, IProductCategoryRepository
    {
        #region Constractor Injection

        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public EditProductCategoryCommand GetDetailForEdit(long id)
        {
            return _context.ProductCategories
                .Select(x => new EditProductCategoryCommand
                {
                    Name = x.Name,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                    Id = x.Id
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> GetByFilter(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories
                .Select(x => new ProductCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ProductsCount = 12
                }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
        }
    }
}
