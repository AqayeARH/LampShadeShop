using System.ComponentModel.DataAnnotations;
using _0.Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.Slider
{
    public class CreateSliderCommand
    {
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "نمیتوانید فایل بیشتر از 5 مگابایت آپلود کنید")]
        [FileExtensionLimitation(new string[] { ".jpg", ".png" }, ErrorMessage = "فقط فایل های jpg و png مجاز هستند")]
        public IFormFile Picture { get; set; }

        [Display(Name = "Picture Alt")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string PictureAlt { get; set; }

        [Display(Name = "Picture Title")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string PictureTitle { get; set; }

        [Display(Name = "سرتیتر")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Heading { get; set; }

        [Display(Name = "عنوان")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "متن")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Text { get; set; }

        [Display(Name = "متن دکمه")]
        [MaxLength(80, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string BtnText { get; set; }

        [Display(Name = "لینک")]
        public string Link { get; set; }
    }
}
