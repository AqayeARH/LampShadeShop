namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class EditArticleCategoryCommand : CreateArticleCategoryCommand
    {
        public long Id { get; set; }
    }
}
