using Lampshade.Query.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        #region Constractor Injection

        private readonly IInventoryQuery _inventoryQuery;
        public InventoryController(IInventoryQuery inventoryQuery)
        {
            _inventoryQuery = inventoryQuery;
        }
        
        #endregion

        [HttpPost]
        public StockStatus CheckStockStatus(IsInStockCommand command)
        {
            return _inventoryQuery.CheckStockStatus(command);
        }
    }
}