namespace Lampshade.Query.Contracts.Inventory
{
    public interface IInventoryQuery
    {
        StockStatus CheckStockStatus(IsInStockCommand command);
    }
}
