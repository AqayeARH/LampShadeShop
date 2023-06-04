using _0.Framework.Application;

namespace ShopManagement.Application.Contracts.ProductComment
{
    public interface IProductCommentApplication
    {
        OperationResult Add(AddCommentCommand command);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);
        List<ProductCommentViewModel> GetList(ProductCommentSearchModel searchModel);
    }
}
