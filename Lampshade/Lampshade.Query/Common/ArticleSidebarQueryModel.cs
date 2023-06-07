using Lampshade.Query.Contracts.Article;
using Lampshade.Query.Contracts.ArticleCategory;

namespace Lampshade.Query.Common
{
    public class ArticleSidebarQueryModel
    {
        public List<ArticleQueryModel> RecentArticles { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
    }
}
