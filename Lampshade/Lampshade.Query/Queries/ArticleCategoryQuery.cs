using BlogManagement.Infra.EfCore;
using Lampshade.Query.Contracts.ArticleCategory;
using Microsoft.EntityFrameworkCore;

namespace Lampshade.Query.Queries
{
    public class ArticleCategoryQuery: IArticleCategoryQuery
    {
        #region Constractor Injection

        private readonly BlogContext _blogContext;
        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        #endregion
        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _blogContext.ArticleCategories
                .Include(x=> x.Articles)
                .OrderBy(x => x.ShowOrder)
                .Select(x => new ArticleCategoryQueryModel()
                {
                    ArticlesCount = x.Articles.Count(y => y.PublishDate <= DateTime.Now),
                    Name = x.Name,
                    Slug = x.Slug
                }).ToList();
        }

        public List<ArticleCategoryQueryModel> GetArticleCategoriesInMenu()
        {
            return _blogContext.ArticleCategories
                .Select(x => new ArticleCategoryQueryModel()
                {
                    Name = x.Name,
                    Slug = x.Slug,
                    ShowOrder = x.ShowOrder
                }).OrderBy(x=> x.ShowOrder).ToList();
        }
    }
}
