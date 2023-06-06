using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lampshade.WebApp.Areas.Administrator.Pages.BlogManagement.Articles
{
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

        public void OnGet(ArticleSearchModel searchModel)
        {
            Articles = _articleApplication.GetList(searchModel);
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }
    }
}
