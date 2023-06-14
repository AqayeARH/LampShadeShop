using _0.Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infra.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Areas.Administrator.Pages.BlogManagement.ArticleCategories
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        #endregion

        public List<ArticleCategoryViewModel> ArticleCategories { get; set; }
        public ArticleCategorySearchModel SearchModel { get; set; }

        [NeedPermission(BlogPermissionCode.ListArticleCategories)]
        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategoryApplication.GetList(searchModel);
        }

        //Handlers

        [NeedPermission(BlogPermissionCode.CreateArticleCategory)]
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateArticleCategoryCommand());
        }

        [NeedPermission(BlogPermissionCode.CreateArticleCategory)]
        public IActionResult OnPostCreate(CreateArticleCategoryCommand command)
        {
            var result = _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        [NeedPermission(BlogPermissionCode.EditArticleCategory)]
        public IActionResult OnGetEdit(long id)
        {
            var model = _articleCategoryApplication.GetForEdit(id);
            return Partial("Edit", model);
        }

        [NeedPermission(BlogPermissionCode.EditArticleCategory)]
        public IActionResult OnPostEdit(EditArticleCategoryCommand command)
        {
            var result = _articleCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
