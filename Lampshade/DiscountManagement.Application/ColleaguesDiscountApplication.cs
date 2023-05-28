using _0.Framework.Application;
using DiscountManagement.Application.Contracts.ColleaguesDiscount;
using DiscountManagement.Domain.ColleaguesDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleaguesDiscountApplication : IColleaguesDiscountApplication
    {
        #region Constractor Injection

        private readonly IColleaguesDiscountRepository _colleaguesDiscountRepository;
        public ColleaguesDiscountApplication(IColleaguesDiscountRepository colleaguesDiscountRepository)
        {
            _colleaguesDiscountRepository = colleaguesDiscountRepository;
        }

        #endregion

        public OperationResult Define(DefineColleaguesDiscountCommand command)
        {
            var operation = new OperationResult();
            if (_colleaguesDiscountRepository.IsExist(x =>
                    x.ProductId == command.ProductId && x.IsRemoved == false))
            {
                return operation.Error("تخفیف همکاری قبلا برای این محصول ثبت شده است");
            }

            var colleaguesDiscount = new ColleaguesDiscount(command.ProductId, command.DiscountRate);
            _colleaguesDiscountRepository.Create(colleaguesDiscount);
            _colleaguesDiscountRepository.Save();

            return operation.Success();
        }

        public OperationResult Edit(EditColleaguesDiscountCommand command)
        {
            var operation = new OperationResult();
            var colleaguesDiscount = _colleaguesDiscountRepository.GetBy(command.Id);

            if (colleaguesDiscount == null)
            {
                return operation.Error("تخفیفی با اطلاعات ارسالی یافت نشد");
            }

            if (_colleaguesDiscountRepository.IsExist(x => 
                    x.Id != command.Id && x.ProductId == command.ProductId && x.IsRemoved == false))
            {
                return operation.Error("تخفیف همکاری قبلا برای این محصول ثبت شده است");
            }

            colleaguesDiscount.Edit(command.ProductId, command.DiscountRate);
            _colleaguesDiscountRepository.Save();

            return operation.Success();
        }

        public EditColleaguesDiscountCommand GetForEdit(long id)
        {
            return _colleaguesDiscountRepository.GetDetailForEdit(id);
        }

        public List<ColleaguesDiscountViewModel> GetList(ColleaguesDiscountSearchModel searchModel)
        {
            return _colleaguesDiscountRepository.GetByFilter(searchModel);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleaguesDiscount = _colleaguesDiscountRepository.GetBy(id);

            if (colleaguesDiscount == null)
            {
                return operation.Error("تخفیفی با اطلاعات ارسالی یافت نشد");
            }

            colleaguesDiscount.Remove();
            _colleaguesDiscountRepository.Save();

            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleaguesDiscount = _colleaguesDiscountRepository.GetBy(id);

            if (colleaguesDiscount == null)
            {
                return operation.Error("تخفیفی با اطلاعات ارسالی یافت نشد");
            }

            if (_colleaguesDiscountRepository.IsExist(x =>
                  x.Id != colleaguesDiscount.Id &&
                  x.ProductId == colleaguesDiscount.ProductId &&
                  x.IsRemoved == false))
            {
                return operation.Error("تخفیف همکاری قبلا برای این محصول ثبت شده است");
            }

            colleaguesDiscount.Restore();
            _colleaguesDiscountRepository.Save();

            return operation.Success();
        }
    }
}
