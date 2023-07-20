using _0.Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lampshade.WebApp.Pages
{
    public class PaymentResultModel : PageModel
    {
	    public string PayStatus { get; set; }
	    public PaymentResult PaymentResult { get; set; }
        public void OnGet(PaymentResult paymentResult)
        {
	        PayStatus = paymentResult.IsSuccessful ? "Å—œ«Œ  „Ê›ﬁ" : "Å—œ«Œ  ‰«„Ê›ﬁ";
	        PaymentResult = paymentResult;
        }
    }
}
