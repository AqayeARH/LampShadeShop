using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class ReduceInventoryCommand
    {
        [Display(Name = "کد انبار")]
        [Range(1, long.MaxValue, ErrorMessage = "لطفا برای کاهش موجودی یک انبار را انتخاب کنید")]
        public long InventoryId { get; set; }

        [Display(Name = "محصول")]
        [Range(1, long.MaxValue, ErrorMessage = "لطفا برای کاهش موجودی یک محصول را انتخاب کنید")]
        public long ProductId { get; set; }

        [Display(Name = "تعداد")]
        [Range(1, long.MaxValue, ErrorMessage = "لطفا برای کاهش موجودی تعداد را انتخاب کنید")]
        public long Count { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "شماره فاکتور")]
        public long OrderId { get; set; }
    }
}
