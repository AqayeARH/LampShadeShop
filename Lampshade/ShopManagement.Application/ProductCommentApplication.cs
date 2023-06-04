using _0.Framework.Application;
using ShopManagement.Application.Contracts.ProductComment;
using ShopManagement.Domain.ProductCommentAgg;

namespace ShopManagement.Application
{
    public class ProductCommentApplication : IProductCommentApplication
    {
        #region Constractor Injection

        private readonly IProductCommentRepository _productCommentRepository;
        public ProductCommentApplication(IProductCommentRepository productCommentRepository)
        {
            _productCommentRepository = productCommentRepository;
        }

        #endregion
        public OperationResult Add(AddCommentCommand command)
        {
            var operation = new OperationResult();

            var comment = new ProductComment(command.ProductId, command.Name, command.Email,
                command.Text);

            _productCommentRepository.Create(comment);
            _productCommentRepository.Save();

            return operation.Success();
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = _productCommentRepository.GetBy(id);

            if (comment == null)
            {
                return operation.Error("نظری با مشخصات ارسالی یافت نشد");
            }

            comment.Confirm();
            _productCommentRepository.Save();

            return operation.Success();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _productCommentRepository.GetBy(id);

            if (comment == null)
            {
                return operation.Error("نظری با مشخصات ارسالی یافت نشد");
            }

            comment.Cancel();
            _productCommentRepository.Save();

            return operation.Success();
        }

        public List<ProductCommentViewModel> GetList(ProductCommentSearchModel searchModel)
        {
            return _productCommentRepository.GetByFilter(searchModel);
        }
    }
}
