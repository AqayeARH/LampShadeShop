using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductComment
{
    public class AddCommentCommand
    {
        [Display(Name = "محصول")]
        [Range(1, long.MaxValue, ErrorMessage = "محصول نا معتبر است")]
        public long ProductId { get; set; }

        [Display(Name = "نام خود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        [MinLength(3 , ErrorMessage = "نام وارد شده خیلی کوتاه است")]
        public string Name { get; set; }

        [Display(Name = "ایمیل خود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string Email { get; set; }

        [Display(Name = "دیدگاه خود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(750, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Text { get; set; }
    }
}
