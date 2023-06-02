using System.ComponentModel.DataAnnotations;
using _0.Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProductCommand
    {
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "دسته بندی محصول")]
        [Range(1, 100000, ErrorMessage = "گروه محصول را انتخاب کنید")]
        public long CategoryId { get; set; }

        [Display(Name = "کد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(15, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Code { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "تصویر محصول")]
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

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Slug { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string KayWords { get; set; }

        [Display(Name = "توضیحات متا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string MetaDescription { get; set; }

        //[Display(Name = "قیمت")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[Range(10, double.MaxValue , ErrorMessage = "یک قیمت برای محصول در نظر بگیرید")]
        //public double UnitPrice { get; set; }

        public List<ProductCategoryViewModel> ProductCategories { get; set; }
    }
}
