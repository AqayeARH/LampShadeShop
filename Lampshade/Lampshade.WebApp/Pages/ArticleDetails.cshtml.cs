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
        public ArticleDetailsModel(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        #endregion

        public ArticleQueryModel Article { get; set; }

        public void OnGet(string slug)
        {
            Article = _articleQuery.GetDetailsBy(slug);
        }
    }
}
