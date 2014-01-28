using System;
using Example.Domain.Barbering.Repository;

namespace Example.Domain.Barbering
{
    public class Locations
    {
        private readonly IShopRepository _shopRepository;

        public Locations(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public Shop OpenForBusiness(string town)
        {
            var shop = _shopRepository.GetBy(town);
            shop.Open(DateTime.Now);

            return shop;
        }
    }
}