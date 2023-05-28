using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DiscountManagement.Application.Contracts.ColleaguesDiscount
{
    public class DefineColleaguesDiscountCommand
    {

        [Range(1, long.MaxValue, ErrorMessage = "یک محصول برای تخفیف انتخاب کنید")]
        [Display(Name = "محصول")]
        public long ProductId { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Range(5, 100, ErrorMessage = "تخفیف باید بین 5 تا 100 درصد باشد")]
        public int DiscountRate { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
