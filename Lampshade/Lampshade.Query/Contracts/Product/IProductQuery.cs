namespace Lampshade.Query.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProducts();
        List<ProductQueryModel> Search(string value);
        ProductQueryModel GetDetailsBy(string slug);
    }
}
