using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace Lampshade.WebApp.Areas.Administrator.Pages.InventoryManagement
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constractor Injection

        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

        #endregion

        public InventorySearchModel SearchModel { get; set; }
        public List<InventoryViewModel> Inventories { get; set; }
        public SelectList Products { get; set; }

        public void OnGet(InventorySearchModel searchModel)
        {
            Inventories = _inventoryApplication.GetList(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        //Handlers
        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateInventoryCommand()
            {
                Products = _productApplication.GetProducts()
            });
        }

        public IActionResult OnPostCreate(CreateInventoryCommand command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var model = _inventoryApplication.GetForEdit(id);
            model.Products = _productApplication.GetProducts();
            return Partial("Edit", model);
        }

        public IActionResult OnPostEdit(EditInventoryCommand command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }
        
        public IActionResult OnGetIncrease(long id)
        {
            var model = new IncreaseInventoryCommand()
            {
                InventoryId = id
            };
            return Partial("Increase", model);
        }

        public IActionResult OnPostIncrease(IncreaseInventoryCommand command)
        {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetReduce(long id)
        {
            return Partial("Reduce", new ReduceInventoryCommand()
            {
                InventoryId = id
            });
        }

        public IActionResult OnPostReduce(ReduceInventoryCommand command)
        {
            var result = _inventoryApplication.Reduce(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetLog(long id)
        {
            var model = _inventoryApplication.GetOperationsLog(id);
            return Partial("OperationLog", model);
        }
    }
}
