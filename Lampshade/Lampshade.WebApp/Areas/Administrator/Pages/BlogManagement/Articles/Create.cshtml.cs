using _0.Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infra.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lampshade.WebApp.Areas.Administrator.Pages.BlogManagement.Articles
{
    [Authorize]
    public class CreateModel : PageModel
    {
        #region Constractor Injection

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public CreateModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        #endregion

        [BindProperty]
        public CreateArticleCommand Command { get; set; }
        public SelectList ArticleCategories { get; set; }

        [NeedPermission(BlogPermissionCode.CreateArticle)]
        public void OnGet()
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }

        [NeedPermission(BlogPermissionCode.CreateArticle)]
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
                return Page();
            }

            _articleApplication.Create(Command);

            return RedirectToPage("Index");
        }
    }
}
