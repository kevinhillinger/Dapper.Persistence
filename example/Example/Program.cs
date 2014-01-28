using Example.Domain.Barbering;
using Example.Domain.Customers;
using Example.Infrastructure;
using StructureMap;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerConfig.Configure();
            var locations = GetLocations();

            var shop = locations.OpenForBusiness("Fargo");

            while (shop.IsOpen)
            {
                var customer = new Customer();
                var barber = shop.GetAvailableBarber();

                barber.GreetCustomer(customer);

                var hairStyle = barber.AskCustomerWhatHairStyleTheyWant();
                var price = shop.GetPriceOfHairCut(hairStyle);

                barber.CutHair();

                var payment = customer.Pay(price);
                barber.TakePayment(payment);
            }
        }

        private static Locations GetLocations()
        {
            return ObjectFactory.GetInstance<Locations>();
        }
    }
}
