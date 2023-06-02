using _0.Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        #region Constractor Injection

        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        #endregion
        public OperationResult Create(CreateProductCommand command)
        {
            var operation = new OperationResult();
            if (_productRepository.IsExist(x => x.Name == command.Name))
            {
                return operation.Error("محصول وارد شده در سایت وجود دارد");
            }

            var slug = command.Slug.FixSlug();

            var picture = _fileUploader.Upload(command.Picture, "products-banner");

            var product = new Product(command.Name, command.CategoryId/*, command.UnitPrice*/, command.Code,
                command.ShortDescription, command.Description, picture, command.PictureAlt,
                command.PictureTitle, slug, command.KayWords, command.MetaDescription);

            _productRepository.Create(product);
            _productRepository.Save();

            return operation.Success();
        }

        public OperationResult Edit(EditProductCommand command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetBy(command.Id);

            if (product == null)
            {
                return operation.Error("محصولی با اطلاعات ارسالی یافت نشد");
            }

            if (_productRepository.IsExist(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Error("محصول وارد شده در سایت وجود دارد");
            }

            var slug = command.Slug.FixSlug();

            var picture = _fileUploader.Upload(command.Picture, "products-banner");

            product.Edit(command.Name, command.CategoryId/*, command.UnitPrice*/, command.Code, command.ShortDescription,
                command.Description, picture, command.PictureAlt, command.PictureTitle, slug,
                command.KayWords, command.MetaDescription);

            _productRepository.Save();
            return operation.Success();
        }

        public EditProductCommand GetForEdit(long id)
        {
            return _productRepository.GetDetailForEdit(id);
        }

        public List<ProductViewModel> GetList(ProductSearchModel searchModel)
        {
            return _productRepository.GetByFilter(searchModel);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        /*
        public OperationResult InStock(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetBy(id);
            if (product == null)
            {
                return operation.Error("محصولی با اطلاعات ارسالی یافت نشد");
            }
            product.InStock();
            _productRepository.Save();
            return operation.Success();
        }

        public OperationResult NotInStock(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetBy(id);
            if (product == null)
            {
                return operation.Error("محصولی با اطلاعات ارسالی یافت نشد");
            }
            product.NotInStock();
            _productRepository.Save();
            return operation.Success();
        }
        */
    }
}
