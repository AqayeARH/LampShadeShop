using _0.Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        #region Constractor Injection

        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        #endregion

        public OperationResult Create(CreateProductCategoryCommand command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.IsExist(x => x.Name == command.Name))
            {
                return operation.Error("این دسته بندی قبلا در سایت وجود دارد");
            }

            var slug = command.Slug.FixSlug();

            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.KeyWords, command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.Save();

            return operation.Success();
        }

        public EditProductCategoryCommand GetForEdit(long id)
        {
            return _productCategoryRepository.GetDetailForEdit(id);
        }

        public OperationResult Edit(EditProductCategoryCommand command)
        {
            var operation = new OperationResult();

            var productCategory = _productCategoryRepository.GetBy(command.Id);

            if (productCategory == null)
            {
                return operation.Error("دسته بندی با اطلاعات ارسالی یافت نشد");
            }

            if (_productCategoryRepository.IsExist(x=>x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Error("این دسته بندی قبلا در سایت وجود دارد");
            }

            var slug = command.Slug.FixSlug();

            productCategory.Edit(command.Name,command.Description,command.Picture,command.PictureAlt,
                command.PictureTitle,command.KeyWords,command.MetaDescription,slug);

            _productCategoryRepository.Save();

            return operation.Success();
        }

        public List<ProductCategoryViewModel> GetList(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.GetByFilter(searchModel);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }
    }
}
