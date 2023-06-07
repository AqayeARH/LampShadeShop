using Lampshade.Query.Contracts.Article;
using _0.Framework.Application;
using BlogManagement.Infra.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Lampshade.Query.Queries
{
    public class ArticleQuery : IArticleQuery
    {
        #region Constractor Injection

        private readonly BlogContext _blogContext;
        public ArticleQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        #endregion
        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _blogContext.Articles
                .Include(x => x.ArticleCategory)
                .Where(x=> x.PublishDate <= DateTime.Now)
                .OrderByDescending(x => x.Id)
                .Take(5)
                .Select(x => new ArticleQueryModel()
                {
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategoryName = x.ArticleCategory.Name,
                    CategorySlug = x.ArticleCategory.Slug,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription
                }).ToList();
        }

        public ArticleQueryModel GetDetailsBy(string slug)
        {
            return _blogContext.Articles.Include(x => x.ArticleCategory)
                .Where(x => x.PublishDate <= DateTime.Now && x.Slug == slug)
                .Select(x => new ArticleQueryModel()
                {
                    KeyWords = x.KeyWords,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Title = x.Title,
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryName = x.ArticleCategory.Name,
                    CategorySlug = x.ArticleCategory.Slug,
                    Description = x.Description,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    MetaDescription = x.MetaDescription,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription
                }).First();
        }

        public List<ArticleQueryModel> GetArticles(string slug)
        {
            var query = _blogContext.Articles
                .Include(x => x.ArticleCategory)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategoryName = x.ArticleCategory.Name,
                    CategorySlug = x.ArticleCategory.Slug,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription.Substring(0,Math.Min(x.ShortDescription.Length,50)) + "...",
                    CategoryMetaDescription = string.IsNullOrEmpty(slug)? "لیست همه ی مقاله های سایت دیجی استور": x.ArticleCategory.MetaDescription,
                    CategoryKeyWords = string.IsNullOrEmpty(slug)?"مقالات سایت, مطالب سایت, همه ی مقالات": x.ArticleCategory.KeyWords
                }).AsQueryable();

            if (!string.IsNullOrEmpty(slug))
            {
                query = query.Where(x => x.CategorySlug == slug);
            }

            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
