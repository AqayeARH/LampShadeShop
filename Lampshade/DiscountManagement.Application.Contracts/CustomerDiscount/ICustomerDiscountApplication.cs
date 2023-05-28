using _0.Framework.Application;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Define(DefineCustomerDiscountCommand command);
        OperationResult Edit(EditCustomerDiscountCommand command);
        EditCustomerDiscountCommand GetForEdit(long id);
        List<CustomerDiscountViewModel> GetList(CustomerDiscountSearchModel searchModel);
    }
}
