using _0.Framework.Application;

namespace BlogManagement.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticleCommand command);
        OperationResult Edit(EditArticleCommand command);
        EditArticleCommand GetForEdit(long id);
        List<ArticleViewModel> GetList(ArticleSearchModel searchModel);
        public List<ArticleViewModel> GetArticles();
    }
}
