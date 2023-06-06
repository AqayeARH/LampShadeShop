namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class ArticleCategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public string Picture { get; set; }
        public int ShowOrder { get; set; }
    }
}
