using System.ComponentModel.DataAnnotations;

namespace CommentManagement.Application.Contracts.Comment
{
    public class AddCommentCommand
    {
        [Range(1, long.MaxValue, ErrorMessage = "سازنده نا معتبر است")]
        public long OwnerRecordId { get; set; }

        [Display(Name = "نوع سازنده")]
        public int Type { get; set; }

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

        public long ParentId { get; set; }
    }
}
