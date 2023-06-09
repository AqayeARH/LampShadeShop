using _0.Framework.Application;
using _0.Framework.Infrastructure;
using BlogManagement.Infra.EfCore;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infra.EfCore;

namespace CommentManagement.Infra.EfCore.Repositories
{
    public class CommentRepository : EfCoreGenericRepository<long, Comment>, ICommentRepository
    {
        #region Constractor Injection

        private readonly CommentingContext _context;
        private readonly ShopContext _shopContext;
        private readonly BlogContext _blogContext;
        public CommentRepository(CommentingContext context, ShopContext shopContext, BlogContext blogContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _blogContext = blogContext;
        }

        #endregion
        public List<CommentViewModel> GetProductCommentsByFilter(CommentSearchModel searchModel)
        {

            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.Comments
                .Where(x => x.Type == CommentTypes.Product)
                .AsEnumerable()
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Text = x.Text,
                    CreationDate = x.CreationDate.ToFarsi(),
                    Status = x.Status,
                    OwnerRecordId = x.OwnerRecordId,
                    Type = x.Type,
                    OwnerRecordName = products.First(p => p.Id == x.OwnerRecordId).Name
                }).AsQueryable();

            #region Filtering

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            if (searchModel.OwnerRecordId > 0)
            {
                query = query.Where(x => x.OwnerRecordId == searchModel.OwnerRecordId);
            }

            #endregion

            return query.OrderByDescending(x => x.Id).ToList();

        }

        public List<CommentViewModel> GetArticleCommentsByFilter(CommentSearchModel searchModel)
        {
            var articles = _blogContext.Articles.Select(x => new { x.Id, x.Title }).ToList();

            var query = _context.Comments
                .Where(x => x.Type == CommentTypes.Article)
                .AsEnumerable()
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Text = x.Text,
                    CreationDate = x.CreationDate.ToFarsi(),
                    Status = x.Status,
                    OwnerRecordId = x.OwnerRecordId,
                    Type = x.Type,
                    OwnerRecordName = articles.First(p => p.Id == x.OwnerRecordId).Title
                }).AsQueryable();

            #region Filtering

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            if (searchModel.OwnerRecordId > 0)
            {
                query = query.Where(x => x.OwnerRecordId == searchModel.OwnerRecordId);
            }

            #endregion

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
