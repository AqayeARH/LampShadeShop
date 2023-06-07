using Lampshade.Query.Contracts.ArticleCategory;
using Lampshade.Query.Contracts.ProductCategory;

namespace Lampshade.Query.Common
{
    public class MenuQueryModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
