using _0.Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        #region Constractor Injection

        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
        }

        #endregion
        public OperationResult Create(CreateProductPictureCommand command)
        {
            var operation = new OperationResult();
            //if (_productPictureRepository.IsExist(x => x.Picture == command.Picture))
            //{
            //    return operation.Error("نام عکس انتخاب شده تکراری است لطفا نام را تغییر دهید");
            //}

            var picture = _fileUploader.Upload(command.Picture, "products-pictures");

            var productPicture = new ProductPicture(command.ProductId, picture, command.PictureAlt,
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

            //if (_productPictureRepository.IsExist(x => x.Picture == command.Picture && x.Id != command.Id))
            //{
            //    return operation.Error("نام عکس انتخاب شده تکراری است لطفا نام را تغییر دهید");
            //}

            var picture = _fileUploader.Upload(command.Picture, "products-pictures");

            productPicture.Edit(command.ProductId, picture, command.PictureAlt, command.PictureTitle);
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
