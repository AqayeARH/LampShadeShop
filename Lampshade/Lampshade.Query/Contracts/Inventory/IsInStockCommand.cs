namespace Lampshade.Query.Contracts.Inventory
{
    public class IsInStockCommand
    {
        public long ProductId { get; set; }
        public int Count { get; set; }
    }
}
