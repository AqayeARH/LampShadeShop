using _0.Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public long CategoryId { get; private set; }
        public ProductCategory Category { get; private set; }
        public string Code { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Slug { get; private set; }
        public string KayWords { get; private set; }
        public string MetaDescription { get; private set; }
        //public double UnitPrice { get; private set; }
        //public bool IsInStock { get; private set; }

        public List<ProductPicture> ProductPictures { get; private set; }

        //The constructor is called when creating a new instance

        protected Product()
        {
            ProductPictures = new List<ProductPicture>();
        }

        public Product(string name, long categoryId/*, double unitPrice*/, string code, string shortDescription,
            string description, string picture, string pictureAlt, string pictureTitle, string slug,
            string kayWords, string metaDescription)
        {
            Name = name;
            CategoryId = categoryId;
            //UnitPrice = unitPrice;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            KayWords = kayWords;
            MetaDescription = metaDescription;
            //IsInStock = true;
        }

        //The edit method is called when an entity is changed
        public void Edit(string name, long categoryId/*, double unitPrice*/, string code, string shortDescription,
            string description, string picture, string pictureAlt, string pictureTitle, string slug,
            string kayWords, string metaDescription)
        {
            Name = name;
            CategoryId = categoryId;
            //UnitPrice = unitPrice;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            if (string.IsNullOrEmpty(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            KayWords = kayWords;
            MetaDescription = metaDescription;
        }

        /*
        public void InStock()
        {
            IsInStock = true;
        }

        public void NotInStock()
        {
            IsInStock = false;
        }
        */
    }
}
