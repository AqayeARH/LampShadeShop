namespace BlogManagement.Application.Contracts.Article
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Picture { get; set; }
        public string PublishDate { get; set; }
    }
}
