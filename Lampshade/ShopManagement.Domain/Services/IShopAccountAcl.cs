namespace ShopManagement.Domain.Services
{
    public interface IShopAccountAcl
    {
        (long accountId, string name, string mobile) GetAccountBy(long id);
    }
}
