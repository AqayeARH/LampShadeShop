using _0.Framework.Domain;
using DiscountManagement.Application.Contracts.CustomerDiscount;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IGenericRepository<long, CustomerDiscount>
    {
        EditCustomerDiscountCommand GetDetailForEdit(long id);
        List<CustomerDiscountViewModel> GetByFilter(CustomerDiscountSearchModel searchModel);
    }
}
