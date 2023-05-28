namespace InventoryManagement.Application.Contracts.Inventory
{
    public class EditInventoryCommand:CreateInventoryCommand
    {
        public long Id { get; set; }
    }
}
