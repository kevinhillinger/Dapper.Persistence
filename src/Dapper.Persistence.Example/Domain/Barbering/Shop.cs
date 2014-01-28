using System.Collections.Generic;

namespace Example.Domain.Barbering
{
    public class Shop
    {
        public ICollection<Barber> Barbers { get; set; } 
    }
}