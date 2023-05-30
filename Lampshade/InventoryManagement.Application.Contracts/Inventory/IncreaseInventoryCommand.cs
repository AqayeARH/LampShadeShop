using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class IncreaseInventoryCommand
    {
        [Display(Name = "توضیحات")]
        [Range(1,long.MaxValue,ErrorMessage = "لطفا برای افزایش موجودی یک انبار را انتخاب کنید")]
        public long InventoryId { get; set; }

        [Display(Name = "توضیحات")]
        [Range(1, long.MaxValue, ErrorMessage = "لطفا برای افزایش موجودی تعداد را انتخاب کنید")]
        public long Count { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Description { get; set; }
    }
}
