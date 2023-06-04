using _0.Framework.Domain;
using ShopManagement.Application.Contracts.ProductComment;

namespace ShopManagement.Domain.ProductCommentAgg
{
    public interface IProductCommentRepository : IGenericRepository<long, ProductComment>
    {
        List<ProductCommentViewModel> GetByFilter(ProductCommentSearchModel searchModel);
    }
}
