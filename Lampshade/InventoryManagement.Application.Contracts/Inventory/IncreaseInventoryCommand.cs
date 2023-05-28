namespace InventoryManagement.Application.Contracts.Inventory
{
    public class IncreaseInventoryCommand
    {
        public long InventoryId { get; set; }
        public long Count { get; set; }
        public string Description { get; set; }
    }
}
