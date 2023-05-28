using System.ComponentModel.DataAnnotations;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class DefineCustomerDiscountCommand
    {
        [Range(1, long.MaxValue, ErrorMessage = "یک محصول برای تخفیف انتخاب کنید")]
        [Display(Name = "محصول")]
        public long ProductId { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Range(5, 100, ErrorMessage = "تخفیف باید بین 5 تا 100 درصد باشد")]
        public int DiscountRate { get; set; }

        [MaxLength(10, ErrorMessage = "تاریخ وارد شده معتبر نمیباشد")]
        [MinLength(10, ErrorMessage = "تاریخ وارد شده معتبر نمیباشد")]
        public string StartDate { get; set; }

        [MaxLength(10, ErrorMessage = "تاریخ وارد شده معتبر نمیباشد")]
        [MinLength(10, ErrorMessage = "تاریخ وارد شده معتبر نمیباشد")]
        public string EndDate { get; set; }

        [Display(Name = "علت تخفیف")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Reason { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
