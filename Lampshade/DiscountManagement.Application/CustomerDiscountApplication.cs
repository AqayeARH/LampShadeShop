using _0.Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        #region Constractor Injection

        private readonly ICustomerDiscountRepository _customerDiscountRepository;
        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        #endregion
        public OperationResult Define(DefineCustomerDiscountCommand command)
        {
            var operation = new OperationResult();

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            if (_customerDiscountRepository.IsExist(x => x.ProductId == command.ProductId && startDate <= DateTime.Now && endDate >= DateTime.Now))
            {
                return operation.Error("این تخفیف با ویژگی های وارد شده برای محصول ثبت شده است");
            }

            var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, startDate, endDate,
                command.Reason);
            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.Save();
            return operation.Success();
        }

        public OperationResult Edit(EditCustomerDiscountCommand command)
        {
            var operation = new OperationResult();

            var customerDiscount = _customerDiscountRepository.GetBy(command.Id);

            if (customerDiscount==null)
            {
                return operation.Error("تخفیفی با اطلاعات ارسالی یافت نشد");
            }

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            if (_customerDiscountRepository.IsExist(x => x.Id != command.Id && x.ProductId == command.ProductId && startDate <= DateTime.Now && endDate >= DateTime.Now))
            {
                return operation.Error("این تخفیف با ویژگی های وارد شده برای محصول ثبت شده است");
            }

            customerDiscount.Edit(command.ProductId, command.DiscountRate, startDate, endDate, command.Reason);
            _customerDiscountRepository.Save();
            return operation.Success();
        }

        public EditCustomerDiscountCommand GetForEdit(long id)
        {
            return _customerDiscountRepository.GetDetailForEdit(id);
        }

        public List<CustomerDiscountViewModel> GetList(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.GetByFilter(searchModel);
        }
    }
}
