using _0.Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture : BaseEntity
    {
        public long ProductId { get; private set; }
        public Product Product { get; private set; }

        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public bool IsRemoved { get; private set; }

        //The constructor is called when creating a new instance
        public ProductPicture(long productId, string picture, string pictureAlt, string pictureTitle)
        {
            ProductId = productId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            IsRemoved = false;
        }

        //The edit method is called when an entity is changed
        public void Edit(long productId, string picture, string pictureAlt, string pictureTitle)
        {
            ProductId = productId;
            if (!string.IsNullOrEmpty(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
