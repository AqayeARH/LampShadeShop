using _0.Framework.Application;
using _0.Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Infra.EfCore.Repositories
{
    public class ArticleCategoryRepository : EfCoreGenericRepository<long, ArticleCategory>, IArticleCategoryRepository
    {
        #region Constractor Injection

        private readonly BlogContext _context;
        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public List<ArticleCategoryViewModel> GetByFilter(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories
                .Select(x => new ArticleCategoryViewModel()
                {
                    CreationDate = x.CreationDate.ToFarsi(),
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    ShowOrder = x.ShowOrder
                }).AsQueryable();

            #region Filtering

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            #endregion

            return query.OrderByDescending(x => x.ShowOrder).ToList();
        }

        public EditArticleCategoryCommand GetDetailForEdit(long id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategoryCommand
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug,
                KeyWords = x.KeyWords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _context.ArticleCategories
                .Select(x => new ArticleCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .OrderByDescending(x => x.Id).ToList();
        }
    }
}
