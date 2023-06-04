namespace Lampshade.Query.Contracts.Product
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Price { get; set; }
        public string PriceAfterDiscount { get; set; }
        public int DiscountRate { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public bool HasDiscount { get; set; }
        public string CategorySlug { get; set; }
        public string ExpireDiscountDate { get; set; }
        public string Code { get; set; }
        public string Tags { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public bool IsInStock { get; set; }
        public List<ProductPicturesQueryModel> ProductPictures { get; set; }
        public List<ProductCommentQueryModel> ProductComments { get; set; }
    }
}
