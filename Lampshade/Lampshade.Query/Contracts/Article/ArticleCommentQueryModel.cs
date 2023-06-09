namespace Lampshade.Query.Contracts.Article
{
    public class ArticleCommentQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public long ParenId { get; set; }
    }
}
