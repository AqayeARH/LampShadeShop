using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slider;

namespace Lampshade.WebApp.Areas.Administrator.Pages.ShopManagement.Sliders
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly ISliderApplication _sliderApplication;
        public IndexModel(ISliderApplication sliderApplication)
        {
            _sliderApplication = sliderApplication;
        }

        #endregion

        public List<SliderViewModel> Sliders { get; set; }

        public void OnGet()
        {
            Sliders = _sliderApplication.GetList();
        }

        //Handlers
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateSliderCommand());
        }

        public IActionResult OnPostCreate(CreateSliderCommand command)
        {
            var result = _sliderApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var model = _sliderApplication.GetForEdit(id);
            return Partial("Edit", model);
        }

        public IActionResult OnPostEdit(EditSliderCommand command)
        {
            var result = _sliderApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            _sliderApplication.Remove(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            _sliderApplication.Restore(id);
            return RedirectToPage("Index");
        }
    }
}
