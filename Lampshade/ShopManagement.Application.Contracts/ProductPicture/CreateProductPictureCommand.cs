using System.ComponentModel.DataAnnotations;
using _0.Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPictureCommand
    {
        [Range(1, long.MaxValue, ErrorMessage = "یک محصول معتبر انتخاب کنید")]
        [Display(Name = "محصول")]
        public long ProductId { get; set; }

        [Display(Name = "تصویر")]
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
        public List<ProductViewModel> Products { get; set; }
    }
}
