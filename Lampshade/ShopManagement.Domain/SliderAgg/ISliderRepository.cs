using _0.Framework.Domain;
using ShopManagement.Application.Contracts.Slider;

namespace ShopManagement.Domain.SliderAgg
{
    public interface ISliderRepository : IGenericRepository<long, Slider>
    {
        EditSliderCommand GetDetailForEdit(long id);
    }
}
