using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Areas.Administrator.Pages
{
    public class IndexModel : PageModel
    {
        public List<Chart> Charts { get; set; }
        public void OnGet()
        {
            
        }
    }

    public class Chart
    {
        public string Label { get; set; }
        public string BackgroundColor { get; set; }
        public List<int> Data { get; set; }
    }
}
