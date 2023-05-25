using _0.Framework.Application;

namespace ShopManagement.Application.Contracts.Slider
{
    public interface ISliderApplication
    {
        OperationResult Create(CreateSliderCommand command);
        OperationResult Edit(EditSliderCommand command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditSliderCommand GetForEdit(long id);
        List<SliderViewModel> GetList();
    }
}
