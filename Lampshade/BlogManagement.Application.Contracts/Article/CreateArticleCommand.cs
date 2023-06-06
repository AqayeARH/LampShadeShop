using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.Article
{
    public class CreateArticleCommand
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "دسته بندی")]
        [Range(1, long.MaxValue, ErrorMessage = "لطفا {0} را وارد کنید")]
        public long CategoryId { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile Picture { get; set; }

        [Display(Name = "Picture Alt")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string PictureAlt { get; set; }

        [Display(Name = "Picture Title")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string PictureTitle { get; set; }

        [Display(Name = "Meta Description")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string MetaDescription { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Slug { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string KeyWords { get; set; }

        [Display(Name = "Canonical Address")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string CanonicalAddress { get; set; }

        [Display(Name = "تاریخ انتشار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PublishDate { get; set; }
    }
}
