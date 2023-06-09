using _0.Framework.Application;

namespace CommentManagement.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult Add(AddCommentCommand command);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);
        List<CommentViewModel> GetProductCommentsList(CommentSearchModel searchModel);
        List<CommentViewModel> GetArticleCommentsList(CommentSearchModel searchModel);
    }
}
