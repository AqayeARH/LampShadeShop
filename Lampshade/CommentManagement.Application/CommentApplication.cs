using _0.Framework.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        #region Constractor Injection

        private readonly ICommentRepository _commentRepository;
        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        #endregion
        public OperationResult Add(AddCommentCommand command)
        {
            var operation = new OperationResult();

            var comment = new Comment(command.OwnerRecordId, command.Type, command.Name, command.Email, command.Text,
                command.ParentId);

            _commentRepository.Create(comment);
            _commentRepository.Save();

            return operation.Success();
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.GetBy(id);

            if (comment == null)
            {
                return operation.Error("نظری با مشخصات ارسالی یافت نشد");
            }

            comment.Confirm();
            _commentRepository.Save();

            return operation.Success();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.GetBy(id);

            if (comment == null)
            {
                return operation.Error("نظری با مشخصات ارسالی یافت نشد");
            }

            comment.Cancel();
            _commentRepository.Save();

            return operation.Success();
        }

        public List<CommentViewModel> GetProductCommentsList(CommentSearchModel searchModel)
        {
            return _commentRepository.GetProductCommentsByFilter(searchModel);
        }

        public List<CommentViewModel> GetArticleCommentsList(CommentSearchModel searchModel)
        {
            return _commentRepository.GetArticleCommentsByFilter(searchModel);
        }
    }
}
