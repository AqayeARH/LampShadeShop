using _0.Framework.Domain;
using DiscountManagement.Application.Contracts.ColleaguesDiscount;

namespace DiscountManagement.Domain.ColleaguesDiscountAgg
{
    public interface IColleaguesDiscountRepository : IGenericRepository<long, ColleaguesDiscount>
    {
        EditColleaguesDiscountCommand GetDetailForEdit(long id);
        List<ColleaguesDiscountViewModel> GetByFilter(ColleaguesDiscountSearchModel searchModel);
    }
}
