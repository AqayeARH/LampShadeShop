using _0.Framework.Domain;
using CommentManagement.Application.Contracts.Comment;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IGenericRepository<long, Comment>
    {
        List<CommentViewModel> GetProductCommentsByFilter(CommentSearchModel searchModel);
        List<CommentViewModel> GetArticleCommentsByFilter(CommentSearchModel searchModel);
    }
}
