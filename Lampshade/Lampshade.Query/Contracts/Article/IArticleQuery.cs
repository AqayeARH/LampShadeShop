namespace Lampshade.Query.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> GetLatestArticles();
        ArticleQueryModel GetDetailsBy(string slug);
        List<ArticleQueryModel> GetArticles(string slug);
    }
}
