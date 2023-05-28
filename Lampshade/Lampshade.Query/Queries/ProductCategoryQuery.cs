using Lampshade.Query.Contracts.ProductCategory;
using ShopManagement.Infra.EfCore;

namespace Lampshade.Query.Queries
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        #region Constractor Injection

        private readonly ShopContext _shopContext;
        public ProductCategoryQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        #endregion
        public List<ProductCategoryQueryModel> ShowCategoriesInIndex()
        {
            return _shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }
    }
}
