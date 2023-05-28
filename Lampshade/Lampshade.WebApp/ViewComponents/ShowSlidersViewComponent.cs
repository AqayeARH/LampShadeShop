using Lampshade.Query.Contracts.Slider;
using Microsoft.AspNetCore.Mvc;

namespace Lampshade.WebApp.ViewComponents
{
    public class ShowSlidersViewComponent : ViewComponent
    {
        private readonly ISliderQuery _sliderQuery;
        public ShowSlidersViewComponent(ISliderQuery sliderQuery)
        {
            _sliderQuery = sliderQuery;
        }

        public IViewComponentResult Invoke()
        {
            var sliders = _sliderQuery.ShowSliders();
            return View("/Pages/Shared/Components/Slider/ShowSliders.cshtml", sliders);
        }
    }
}
