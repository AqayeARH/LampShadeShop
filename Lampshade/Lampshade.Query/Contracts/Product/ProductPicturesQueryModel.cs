namespace Lampshade.Query.Contracts.Product
{
    public class ProductPicturesQueryModel
    {
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public bool IsRemoved { get; set; }
    }
}
