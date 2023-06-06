using _0.Framework.Domain;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : BaseEntity
    {
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }
        public string Slug { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }

        public List<Article> Articles { get; private set; }

        //Empty constructor for set relations
        protected ArticleCategory()
        {
            Articles = new List<Article>();
        }

        //The constructor is called when creating a new instance
        public ArticleCategory(string name, string picture, string description, int showOrder, string slug,
            string pictureAlt, string pictureTitle, string keyWords, string metaDescription, string canonicalAddress)
        {
            Name = name;
            Picture = picture;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }

        //The edit method is called when an entity is changed
        public void Edit(string name, string picture, string description, int showOrder, string slug,
            string pictureAlt, string pictureTitle, string keyWords, string metaDescription, string canonicalAddress)
        {
            Name = name;
            if (!string.IsNullOrEmpty(picture))
            {
                Picture = picture;
            }
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }
    }
}
