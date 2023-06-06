using _0.Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article : BaseEntity
    {
        public string Title { get; private set; }
        public long CategoryId { get; private set; }
        public ArticleCategory ArticleCategory { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public string KeyWords { get; private set; }
        public string CanonicalAddress { get; private set; }
        public DateTime PublishDate { get; private set; }

        //Empty constructor
        protected Article()
        {
            
        }

        //The constructor is called when creating a new instance
        public Article(string title, long categoryId, string shortDescription, string description,
            string picture, string pictureAlt, string pictureTitle, string metaDescription, string slug,
            string keyWords, string canonicalAddress, DateTime publishDate)
        {
            Title = title;
            CategoryId = categoryId;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            MetaDescription = metaDescription;
            Slug = slug;
            KeyWords = keyWords;
            CanonicalAddress = canonicalAddress;
            PublishDate = publishDate;
        }

        //The edit method is called when an entity is changed
        public void Edit(string title, long categoryId, string shortDescription, string description,
            string picture, string pictureAlt, string pictureTitle, string metaDescription, string slug,
            string keyWords, string canonicalAddress, DateTime publishDate)
        {
            Title = title;
            CategoryId = categoryId;
            ShortDescription = shortDescription;
            Description = description;
            if (!string.IsNullOrEmpty(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            MetaDescription = metaDescription;
            Slug = slug;
            KeyWords = keyWords;
            CanonicalAddress = canonicalAddress;
            PublishDate = publishDate;
        }
    }
}
