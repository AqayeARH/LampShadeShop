﻿using _0.Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public List<Product> Products { get; private set; }

        //Empty constructor
        protected ProductCategory()
        {
            Products = new List<Product>();
        }

        //The constructor is called when creating a new instance
        public ProductCategory(string name, string description, string picture, string pictureAlt, string pictureTitle, string keyWords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        //The edit method is called when an entity is changed
        public void Edit(string name, string description, string picture, string pictureAlt, string pictureTitle, string keyWords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
