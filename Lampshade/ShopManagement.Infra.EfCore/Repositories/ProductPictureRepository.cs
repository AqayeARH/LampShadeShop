using _0.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infra.EfCore.Repositories
{
    public class ProductPictureRepository:EfCoreGenericRepository<long,ProductPicture> , IProductPictureRepository
    {
        #region Constractor Injection

        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public EditProductPictureCommand GetDetailForEdit(long id)
        {
            return _context.ProductPictures
                .Select(x => new EditProductPictureCommand
                {
                    ProductId = x.ProductId,
                    //Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Id = x.Id
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> GetByFilter(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures
                .Include(x=>x.Product)
                .Select(x => new ProductPictureViewModel
                {
                    Id = x.Id,
                    ProductName = x.Product.Name,
                    Picture = x.Picture,
                    ProductId = x.ProductId,
                    IsRemoved = x.IsRemoved
                }).AsQueryable();

            #region Filtering

            if (searchModel.ProductId!=0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            #endregion

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
