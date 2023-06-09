using BlogManagement.Application.Contracts.Article;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lampshade.WebApp.Areas.Administrator.Pages.CommentsManagement.ArticlesComments
{
    public class IndexModel : PageModel
    {
        #region Constractoe Injection

        private readonly ICommentApplication _commentApplication;
        private readonly IArticleApplication _articleApplication;
        public IndexModel(ICommentApplication commentApplication, IArticleApplication articleApplication)
        {
            _commentApplication = commentApplication;
            _articleApplication = articleApplication;
        }

        #endregion

        public List<CommentViewModel> ArticleComments { get; set; }
        public CommentSearchModel SearchModel { get; set; }
        public SelectList Articles { get; set; }

        public void OnGet(CommentSearchModel searchModel)
        {
            ArticleComments = _commentApplication.GetArticleCommentsList(searchModel);
            Articles = new SelectList(_articleApplication.GetArticles(), "Id", "Title");
        }

        //Handlers
        public IActionResult OnGetConfirm(long id)
        {
            _commentApplication.Confirm(id);

            return RedirectToPage("Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            _commentApplication.Cancel(id);

            return RedirectToPage("Index");
        }
    }
}
