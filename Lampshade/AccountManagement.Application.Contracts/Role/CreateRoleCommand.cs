using System.ComponentModel.DataAnnotations;
using _0.Framework.Infrastructure;

namespace AccountManagement.Application.Contracts.Role
{
    public class CreateRoleCommand
    {
        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Name { get; set; }

        public List<int> Permissions { get; set; }
    }
}
