namespace Lampshade.Query.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        List<ArticleCategoryQueryModel> GetArticleCategories();
        List<ArticleCategoryQueryModel> GetArticleCategoriesInMenu();
    }
}
