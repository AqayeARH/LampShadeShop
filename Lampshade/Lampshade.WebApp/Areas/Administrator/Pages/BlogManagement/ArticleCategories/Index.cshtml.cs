using BlogManagement.Application.Contracts.ArticleCategory;
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

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategoryApplication.GetList(searchModel);
        }

        //Handlers
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateArticleCategoryCommand());
        }

        public IActionResult OnPostCreate(CreateArticleCategoryCommand command)
        {
            var result = _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var model = _articleCategoryApplication.GetForEdit(id);
            return Partial("Edit", model);
        }

        public IActionResult OnPostEdit(EditArticleCategoryCommand command)
        {
            var result = _articleCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
