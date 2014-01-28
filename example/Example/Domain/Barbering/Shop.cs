using System;
using System.Collections.Generic;
using Example.Domain.Customers;

namespace Example.Domain.Barbering
{
    public class Shop
    {
        public ICollection<Barber> Barbers { get; set; }
        public string Town { get; set; }

        public bool IsOpen { get; set; }

        /// <summary>
        /// This is just a trivial example, so we're going to set this to seconds
        /// </summary>
        public int BusinessHours { get; set; }

        public void Open(DateTime now)
        {
            
        }

        public Barber GetAvailableBarber()
        {
            throw new NotImplementedException();
        }

        public decimal GetPriceOfHairCut(HairStyle style)
        {
            return 10.00m;
        }

    }
}