namespace Example.Domain.Barbering.Repository
{
    public interface IShopRepository
    {
        Shop GetBy(string town);
    }
}