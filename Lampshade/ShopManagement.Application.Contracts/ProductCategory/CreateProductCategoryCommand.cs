using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using _0.Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategoryCommand
    {
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "تصویر")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "نمیتوانید فایل بیشتر از 5 مگابایت آپلود کنید")]
        [FileExtensionLimitation(new string[] { ".jpg", ".png" },ErrorMessage = "فقط فایل های jpg و png مجاز هستند")]
        public IFormFile Picture { get; set; }

        [Display(Name = "Picture Alt")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string PictureAlt { get; set; }

        [Display(Name = "Picture Title")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string PictureTitle { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(80, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string KeyWords { get; set; }

        [Display(Name = "توضیحات متا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string MetaDescription { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Slug { get; set; }
    }
}
