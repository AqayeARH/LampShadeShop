using _0.Framework.Infrastructure;

namespace InventoryManagement.Infra.Configuration.Permissions
{
    public class InventoryPermissionExposure:IPermissionExposure
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "Inventory", new List<PermissionDto>()
                    {
                        new PermissionDto(InventoryPermissionCode.ListInventory, "ListInventories"),
                        new PermissionDto(InventoryPermissionCode.CreateInventory, "CreateInventory"),
                        new PermissionDto(InventoryPermissionCode.EditInventory, "EditInventory"),
                        new PermissionDto(InventoryPermissionCode.IncreaseInventory, "IncreaseInventory"),
                        new PermissionDto(InventoryPermissionCode.ReduceInventory, "ReduceInventory"),
                        new PermissionDto(InventoryPermissionCode.LogInventory, "LogInventory")
                    }
                },
            };
        }
    }
}
