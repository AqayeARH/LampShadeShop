using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lampshade.WebApp.Areas.Administrator.Pages.BlogManagement.Articles
{
    [Authorize]
    public class EditModel : PageModel
    {
        #region Constractor Injection

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public EditModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        #endregion

        [BindProperty]
        public EditArticleCommand Command { get; set; }
        public SelectList ArticleCategories { get; set; }

        public IActionResult OnGet(long id)
        {
            Command = _articleApplication.GetForEdit(id);
            if (Command == null)
            {
                return NotFound();
            }
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
                return Page();
            }
            _articleApplication.Edit(Command);
            return RedirectToPage("Index");
        }
    }
}
