using _0.Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        #region Constractor Injection

        private readonly IProductPictureRepository _productPictureRepository;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        #endregion
        public OperationResult Create(CreateProductPictureCommand command)
        {
            var operation = new OperationResult();
            if (_productPictureRepository.IsExist(x => x.Picture == command.Picture))
            {
                return operation.Error("نام عکس انتخاب شده تکراری است لطفا نام را تغییر دهید");
            }

            var productPicture = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.Save();

            return operation.Success();
        }

        public OperationResult Edit(EditProductPictureCommand command)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.GetBy(command.Id);

            if (productPicture == null)
            {
                return operation.Error("عکسی با اطلاعات ارسالی یافت نشد");
            }

            if (_productPictureRepository.IsExist(x => x.Picture == command.Picture && x.Id != command.Id))
            {
                return operation.Error("نام عکس انتخاب شده تکراری است لطفا نام را تغییر دهید");
            }

            productPicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.Save();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.GetBy(id);

            if (productPicture == null)
            {
                return operation.Error("عکسی با اطلاعات ارسالی یافت نشد");
            }

            productPicture.Remove();
            _productPictureRepository.Save();

            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.GetBy(id);

            if (productPicture == null)
            {
                return operation.Error("عکسی با اطلاعات ارسالی یافت نشد");
            }

            productPicture.Restore();
            _productPictureRepository.Save();

            return operation.Success();
        }

        public EditProductPictureCommand GetForEdit(long id)
        {
            return _productPictureRepository.GetDetailForEdit(id);
        }

        public List<ProductPictureViewModel> GetList(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.GetByFilter(searchModel);
        }
    }
}
