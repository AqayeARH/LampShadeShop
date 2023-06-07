using Lampshade.Query.Common;
using Lampshade.Query.Contracts.Article;
using Lampshade.Query.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;

namespace Lampshade.WebApp.ViewComponents
{
    public class ArticleSidebarViewComponent : ViewComponent
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;
        public ArticleSidebarViewComponent(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = new ArticleSidebarQueryModel()
            {
                ArticleCategories = _articleCategoryQuery.GetArticleCategories(),
                RecentArticles = _articleQuery.GetLatestArticles()
            };
            return View("/Pages/Shared/Components/Articles/ArticleSidebar.cshtml",model);
        }
    }
}
