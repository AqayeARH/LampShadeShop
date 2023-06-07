namespace Lampshade.Query.Contracts.ArticleCategory
{
    public class ArticleCategoryQueryModel
    {
        public string Name { get; set; }
        public long ArticlesCount { get; set; }
        public string Slug { get; set; }
        public int ShowOrder { get; set; }
    }
}
