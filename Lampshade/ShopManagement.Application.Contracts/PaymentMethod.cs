namespace ShopManagement.Application.Contracts
{
    public class PaymentMethod
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private PaymentMethod(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public static List<PaymentMethod> GetPaymentMethodsList()
        {
            return new List<PaymentMethod>()
            {
                new PaymentMethod(1, "پرداخت آنلاین", "پرداخت آنلاین مبلغ از طریق درگاه پرداخت زرین پال"),
                new PaymentMethod(2, "پرداخت نقدی", "پرداخت مبلغ پس از تحویل کالا"),
            };
        }

        public static PaymentMethod GetPaymentMethodBy(long id)
        {
            return GetPaymentMethodsList().FirstOrDefault(x => x.Id == id);
        }
    }
}
