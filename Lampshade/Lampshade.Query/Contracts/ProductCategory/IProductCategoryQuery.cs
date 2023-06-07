namespace Lampshade.Query.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> ShowCategoriesInIndex();
        List<ProductCategoryQueryModel> GetCategoriesWithProductsInIndex();
        ProductCategoryQueryModel GetCategoryWithProducts(string slug);
        List<ProductCategoryQueryModel> GetProductCategoriesInMenu();
    }
}
