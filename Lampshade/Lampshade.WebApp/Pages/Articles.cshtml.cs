using Lampshade.Query.Contracts.Article;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Pages
{
    public class ArticlesModel : PageModel
    {
        #region Constractor Injection

        private readonly IArticleQuery _articleQuery;
        public ArticlesModel(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        #endregion

        public List<ArticleQueryModel> Articles { get; set; }

        public void OnGet(string slug = "")
        {
            if (string.IsNullOrEmpty(slug))
            {
                ViewData["category"] = "همه ی مقالات";
            }
            else
            {
                ViewData["category"] = slug.Replace("-", " ");
            }
            Articles = _articleQuery.GetArticles(slug);
        }
    }
}
