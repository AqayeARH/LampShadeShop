using _0.Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IGenericRepository<long, ArticleCategory>
    {
        List<ArticleCategoryViewModel> GetByFilter(ArticleCategorySearchModel searchModel);
        EditArticleCategoryCommand GetDetailForEdit(long id);
        List<ArticleCategoryViewModel> GetArticleCategories();
    }
}
