using _0.Framework.Application;
using _0.Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infra.EfCore.Repositories
{
    public class ArticleRepository : EfCoreGenericRepository<long, Article>, IArticleRepository
    {
        #region Constractor Injection

        private readonly BlogContext _context;
        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public EditArticleCommand GetDetailForEdit(long id)
        {
            return _context.Articles
                .Select(x => new EditArticleCommand()
                {
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    Id = x.Id,
                    Slug = x.Slug,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription,
                    Title = x.Title
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> GetByFilter(ArticleSearchModel searchModel)
        {
            var query = _context.Articles
                .Include(x => x.ArticleCategory)
                .Select(x => new ArticleViewModel()
                {
                    PublishDate = x.PublishDate.ToFarsi(),
                    Title = x.Title,
                    CategoryId = x.CategoryId,
                    CategoryName = x.ArticleCategory.Name,
                    Id = x.Id,
                    Picture = x.Picture
                }).AsQueryable();

            #region Filtering

            if (!string.IsNullOrEmpty(searchModel.Title))
            {
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            }

            if (searchModel.CategoryId > 0)
            {
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            }

            #endregion

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
