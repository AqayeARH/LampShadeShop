using _0.Framework.Application;
using ShopManagement.Application.Contracts.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Application
{
    public class SliderApplication : ISliderApplication
    {
        #region Constractor Injection

        private readonly ISliderRepository _sliderRepository;
        private readonly IFileUploader _fileUploader;

        public SliderApplication(ISliderRepository sliderRepository, IFileUploader fileUploader)
        {
            _sliderRepository = sliderRepository;
            _fileUploader = fileUploader;
        }

        #endregion
        public OperationResult Create(CreateSliderCommand command)
        {
            var operation = new OperationResult();

            var picture = _fileUploader.Upload(command.Picture, "sliders");

            var slider = new Slider(picture, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText,command.Link);

            _sliderRepository.Create(slider);
            _sliderRepository.Save();
            return operation.Success();
        }

        public OperationResult Edit(EditSliderCommand command)
        {
            var operation = new OperationResult();

            var slider = _sliderRepository.GetBy(command.Id);
            if (slider == null)
            {
                return operation.Error("اسلایدری با مشخصات ارسالی یافت نشد");
            }

            var picture = _fileUploader.Upload(command.Picture, "sliders");

            slider.Edit(picture, command.PictureAlt, command.PictureTitle, command.Heading, command.Title,
                command.Text, command.BtnText,command.Link);
            _sliderRepository.Save();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var slider = _sliderRepository.GetBy(id);
            if (slider == null)
            {
                return operation.Error("اسلایدری با مشخصات ارسالی یافت نشد");
            }

            slider.Remove();

            _sliderRepository.Save();
            return operation.Success();
        }
        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var slider = _sliderRepository.GetBy(id);
            if (slider == null)
            {
                return operation.Error("اسلایدری با مشخصات ارسالی یافت نشد");
            }

            slider.Restore();

            _sliderRepository.Save();
            return operation.Success();
        }
        public EditSliderCommand GetForEdit(long id)
        {
            return _sliderRepository.GetDetailForEdit(id);
        }

        public List<SliderViewModel> GetList()
        {
            return _sliderRepository.GetAll()
                .Select(x => new SliderViewModel()
                {
                    CreationDate = x.CreationDate.ToFarsi(),
                    Heading = x.Heading,
                    Id = x.Id,
                    Picture = x.Picture,
                    Title = x.Title,
                    IsRemoved = x.IsRemoved
                }).ToList();
        }
    }
}
