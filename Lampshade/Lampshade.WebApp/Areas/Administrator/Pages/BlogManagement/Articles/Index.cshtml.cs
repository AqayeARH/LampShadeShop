using _0.Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infra.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lampshade.WebApp.Areas.Administrator.Pages.BlogManagement.Articles
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        #endregion

        public List<ArticleViewModel> Articles { get; set; }
        public ArticleSearchModel SearchModel { get; set; }
        public SelectList ArticleCategories { get; set; }

        [NeedPermission(BlogPermissionCode.ListArticles)]
        public void OnGet(ArticleSearchModel searchModel)
        {
            Articles = _articleApplication.GetList(searchModel);
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }
    }
}
