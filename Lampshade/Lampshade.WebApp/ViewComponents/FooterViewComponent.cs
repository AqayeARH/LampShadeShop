using Microsoft.AspNetCore.Mvc;

namespace Lampshade.WebApp.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("/Pages/Shared/Components/Footer/Footer.cshtml");
        }
    }
}
