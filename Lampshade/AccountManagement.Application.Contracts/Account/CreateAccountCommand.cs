using System.ComponentModel.DataAnnotations;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account
{
    public class CreateAccountCommand
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Fullname { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "{0} باید شامل حروف انگلیسی یا عدد باشد")]
        public string Username { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "{0} باید شامل حروف انگلیسی یا عدد باشد")]
        public string Password { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        [MinLength(11, ErrorMessage = "{0} نمیتواند کمتر از {1} کراکتر باشد")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "{0} باید فقط عدد باشد")]
        public string Mobile { get; set; }
        public IFormFile Profile { get; set; }

        [Display(Name = "نقش")]
        [Range(1,long.MaxValue,ErrorMessage = "لطفا {0} را وارد کنید")]
        public long RoleId { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
