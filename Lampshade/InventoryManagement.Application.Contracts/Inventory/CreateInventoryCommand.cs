using System.ComponentModel.DataAnnotations;
using ShopManagement.Application.Contracts.Product;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class CreateInventoryCommand
    {
        [Range(1,long.MaxValue,ErrorMessage = "لطفا یک محصول برای انبار انتخاب کنید")]
        public long ProductId { get; set; }

        [Range(1,long.MaxValue,ErrorMessage = "لطفا قیمت را وارد کنید")]
        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
