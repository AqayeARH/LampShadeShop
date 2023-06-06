using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class CreateArticleCategoryCommand
    {
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile Picture { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "ترتیب نمایش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ShowOrder { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Slug { get; set; }

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
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string KeyWords { get; set; }

        [Display(Name = "توضحیات متا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string MetaDescription { get; set; }

        [Display(Name = "Canonical Address")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string CanonicalAddress { get; set; }
    }
}
