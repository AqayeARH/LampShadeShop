using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
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

        public void OnGet()
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }

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
