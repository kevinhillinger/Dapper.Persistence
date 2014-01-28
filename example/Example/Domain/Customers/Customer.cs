namespace Example.Domain.Customers
{
    public class Customer
    {
        public Hair Hair { get; set; }
        public decimal Wallet { get; set; }

        public decimal Pay(decimal amount)
        {
            Wallet -= Wallet - amount;
            return amount;
        }
    }
}