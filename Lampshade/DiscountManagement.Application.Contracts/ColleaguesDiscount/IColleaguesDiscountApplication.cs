using _0.Framework.Application;

namespace DiscountManagement.Application.Contracts.ColleaguesDiscount
{
    public interface IColleaguesDiscountApplication
    {
        OperationResult Define(DefineColleaguesDiscountCommand command);
        OperationResult Edit(EditColleaguesDiscountCommand command);
        EditColleaguesDiscountCommand GetForEdit(long id);
        List<ColleaguesDiscountViewModel> GetList(ColleaguesDiscountSearchModel searchModel);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
    }
}
