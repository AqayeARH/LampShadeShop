using _0.Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : BaseEntity
    {
        /*
        public long Count { get; private set; }
        */
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }

        public List<InventoryOperation> InventoryOperations { get; private set; }

        protected Inventory()
        {
            InventoryOperations = new List<InventoryOperation>();
        }

        //The constructor is called when creating a new instance
        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public long CalculateCurrentCountInStock()
        {
            var plus = InventoryOperations.Where(x => x.Operation == true)
                .Sum(x => x.Count);      //Calculation of inputs

            var minus = InventoryOperations.Where(x => x.Operation == false)
                .Sum(x => x.Count);     //Calculation of outputs

            return plus - minus;        //Calculate the number in stock
        }

        public void Increase(long count, long operatorId, string description)
        {
            var currentCount = CalculateCurrentCountInStock() + count;

            var operation = new InventoryOperation(this.Id, true, count, operatorId, currentCount, description, 0);
            InventoryOperations.Add(operation);

            InStock = currentCount > 0;

            //Hint
            /*
             * When increasing the stock,
             * we set the invoice number to zero because only the store employee can increase the stock
             */
        }

        public void Reduce(long count, long operatorId, string description, long orderId)
        {
            var currentCount = CalculateCurrentCountInStock() - count;

            var operation = new InventoryOperation(this.Id, false, count, operatorId, currentCount, description, orderId);
            InventoryOperations.Add(operation);

            InStock = currentCount > 0;
        }
    }
}
