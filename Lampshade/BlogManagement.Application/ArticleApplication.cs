using _0.Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        #region Constractor Injection

        private readonly IArticleRepository _articleRepository;
        private readonly IFileUploader _fileUploader;
        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
        }

        #endregion

        public OperationResult Create(CreateArticleCommand command)
        {
            var operation = new OperationResult();
            var slug = command.Slug.FixSlug();
            if (_articleRepository.IsExist(x => x.Slug == slug || x.Title == command.Title))
            {
                return operation.Error("امکان ثبت مقاله با عنوان و slug تکراری وجود ندارد.");
            }

            var picture = _fileUploader.Upload(command.Picture, "articles");

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new Article(command.Title, command.CategoryId, command.ShortDescription, command.Description,
                picture, command.PictureAlt, command.PictureTitle, command.MetaDescription, command.Slug,
                command.KeyWords, command.CanonicalAddress, publishDate);

            _articleRepository.Create(article);
            _articleRepository.Save();

            return operation.Success();
        }

        public OperationResult Edit(EditArticleCommand command)
        {
            var operation = new OperationResult();

            var article = _articleRepository.GetBy(command.Id);
            if (article == null)
            {
                return operation.Error("مقاله ای با مشخصات ارسالی یافت نشد");
            }

            var slug = command.Slug.FixSlug();
            if (_articleRepository.IsExist(x => (x.Slug == slug || x.Title == command.Title) 
                           && x.Id != command.Id))
            {
                return operation.Error("امکان ثبت مقاله با عنوان و slug تکراری وجود ندارد.");
            }

            var picture = _fileUploader.Upload(command.Picture, "articles");

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title, command.CategoryId, command.ShortDescription, command.Description,
                picture, command.PictureAlt, command.PictureTitle, command.MetaDescription, command.Slug,
                command.KeyWords, command.CanonicalAddress, publishDate);
            _articleRepository.Save();
            return operation.Success();
        }

        public EditArticleCommand GetForEdit(long id)
        {
            return _articleRepository.GetDetailForEdit(id);
        }

        public List<ArticleViewModel> GetList(ArticleSearchModel searchModel)
        {
            return _articleRepository.GetByFilter(searchModel);
        }
    }
}
