namespace InventoryManagement.Domain.InventoryAgg;

public class InventoryOperation
{
    public long Id { get; private set; }
    public long InventoryId { get; private set; }
    public bool Operation { get; private set; }
    public long Count { get; private set; }
    public long OperatorId { get; private set; }
    public DateTime OperationDate { get; private set; }
    public long CurrentCount { get; private set; }
    public string Description { get; private set; }
    public long OrderId { get; private set; }

    public Inventory Inventory { get; private set; }

    protected InventoryOperation()
    {
        
    }

    //The constructor is called when creating a new instance
    public InventoryOperation(long inventoryId, bool operation, long count, long operatorId, long currentCount, string description, long orderId)
    {
        InventoryId = inventoryId;
        Operation = operation;
        Count = count;
        OperatorId = operatorId;
        CurrentCount = currentCount;
        Description = description;
        OrderId = orderId;
        OperationDate = DateTime.Now;
    }
}