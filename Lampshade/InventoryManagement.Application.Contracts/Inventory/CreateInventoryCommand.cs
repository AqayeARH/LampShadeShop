namespace InventoryManagement.Application.Contracts.Inventory
{
    public class CreateInventoryCommand
    {
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
    }
}
