using _0.Framework.Infrastructure;

namespace BlogManagement.Infra.Configuration.Permissions
{
    public class BlogPermissionExposure:IPermissionExposure
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "ArticleCategory", new List<PermissionDto>()
                    {
                        new PermissionDto(BlogPermissionCode.ListArticleCategories, "ListArticleCategories"),
                        new PermissionDto(BlogPermissionCode.CreateArticleCategory, "CreateArticleCategory"),
                        new PermissionDto(BlogPermissionCode.EditArticleCategory, "EditArticleCategory"),
                    }
                },
                {
                    "Article", new List<PermissionDto>()
                    {
                        new PermissionDto(BlogPermissionCode.ListArticles, "ListArticles"),
                        new PermissionDto(BlogPermissionCode.CreateArticle, "CreateArticle"),
                        new PermissionDto(BlogPermissionCode.EditArticle, "EditArticle"),
                    }
                },
            };
        }
    }
}
