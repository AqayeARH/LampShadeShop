using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using Lampshade.Query.Contracts.Article;
using Lampshade.Query.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Pages
{
    public class ArticleDetailsModel : PageModel
    {
        #region Constractor Injection

        private readonly IArticleQuery _articleQuery;
        private readonly ICommentApplication _commentApplication;
        public ArticleDetailsModel(IArticleQuery articleQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _commentApplication = commentApplication;
        }

        #endregion

        public ArticleQueryModel Article { get; set; }

        [BindProperty]
        public AddCommentCommand Command { get; set; }

        public void OnGet(string slug)
        {
            Article = _articleQuery.GetDetailsBy(slug);
        }

        public IActionResult OnPost(string articleSlug)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Command.Type = CommentTypes.Article;
            _commentApplication.Add(Command);

            return RedirectToPage("ArticleDetails", new { slug = articleSlug });
        }
    }
}
