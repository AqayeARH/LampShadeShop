using _0.Framework.Application;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public interface IArticleCategoryApplication
    {
        OperationResult Create(CreateArticleCategoryCommand command);
        OperationResult Edit(EditArticleCategoryCommand command);
        EditArticleCategoryCommand GetForEdit(long id);
        List<ArticleCategoryViewModel> GetList(ArticleCategorySearchModel searchModel);
        List<ArticleCategoryViewModel> GetArticleCategories();
    }
}
