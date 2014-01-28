using Example.Domain.Customers;

namespace Example.Domain.Barbering
{
    public class Barber
    {
        private Customer _customer;

        public string Name { get; set; }

        public void GreetCustomer(Customer customer)
        {
            _customer = customer;
        }

        public void CutHair()
        {
            //cut the customer hair
        }

        public void TakePayment(decimal money)
        {
            
        }

        public HairStyle AskCustomerWhatHairStyleTheyWant()
        {
            throw new System.NotImplementedException();
        }

        public decimal GetPriceOfHairCut(HairStyle style)
        {
            return 10.00m;
        }

    }
}