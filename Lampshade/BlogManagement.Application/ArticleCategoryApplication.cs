using _0.Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        #region Constractor Injection

        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        #endregion
        public OperationResult Create(CreateArticleCategoryCommand command)
        {
            var operation = new OperationResult();
            var slug = command.Slug.FixSlug();
            if (_articleCategoryRepository.IsExist(x => x.Slug == slug))
            {
                return operation.Error("قبلا دسته بندی با این Slug در سایت ثبت شده است.");
            }

            var picture = _fileUploader.Upload(command.Picture, "articleCategories");

            var articleCategory = new ArticleCategory(command.Name, picture, command.Description,
                command.ShowOrder, slug, command.PictureAlt, command.PictureTitle, command.KeyWords,
                command.MetaDescription, command.CanonicalAddress);

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.Save();

            return operation.Success();
        }

        public OperationResult Edit(EditArticleCategoryCommand command)
        {
            var operation = new OperationResult();
            var articleCategory = _articleCategoryRepository.GetBy(command.Id);

            if (articleCategory == null)
            {
                return operation.Error("دسته بندی ای برای مقالات با مشخصات ارسالی یافت نشد");
            }

            var slug = command.Slug.FixSlug();

            if (_articleCategoryRepository.IsExist(x => x.Slug == slug && x.Id != command.Id))
            {
                return operation.Error("قبلا دسته بندی با این Slug در سایت ثبت شده است.");
            }

            var picture = _fileUploader.Upload(command.Picture, "articleCategories");

            articleCategory.Edit(command.Name, picture, command.Description, command.ShowOrder, slug,
                command.PictureAlt, command.PictureTitle, command.KeyWords, command.MetaDescription,
                command.CanonicalAddress);

            _articleCategoryRepository.Save();

            return operation.Success();
        }

        public EditArticleCategoryCommand GetForEdit(long id)
        {
            return _articleCategoryRepository.GetDetailForEdit(id);
        }

        public List<ArticleCategoryViewModel> GetList(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.GetByFilter(searchModel);
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }
    }
}
