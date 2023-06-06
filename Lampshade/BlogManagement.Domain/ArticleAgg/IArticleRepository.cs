using _0.Framework.Domain;
using BlogManagement.Application.Contracts.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IGenericRepository<long, Article>
    {
        EditArticleCommand GetDetailForEdit(long id);
        List<ArticleViewModel> GetByFilter(ArticleSearchModel searchModel);
    }
}
