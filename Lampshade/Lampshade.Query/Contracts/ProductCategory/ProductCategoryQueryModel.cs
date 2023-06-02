using Lampshade.Query.Contracts.Product;

namespace Lampshade.Query.Contracts.ProductCategory
{
    public class ProductCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public string MetaDescription { get; set; }
        public string KeyWords { get; set; }

        public List<ProductQueryModel> CategoryProducts { get; set; }
    }
}
