using _0.Framework.Infrastructure;
using ShopManagement.Application.Contracts.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Infra.EfCore.Repositories
{
    public class SliderRepository : EfCoreGenericRepository<long, Slider>, ISliderRepository
    {
        #region Constractor Injection

        private readonly ShopContext _context;

        public SliderRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion
        public EditSliderCommand GetDetailForEdit(long id)
        {
            return _context.Sliders.Select(x => new EditSliderCommand
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Heading = x.Heading,
                Title = x.Title,
                Text = x.Text,
                BtnText = x.BtnText,
                Id = x.Id,
                Link = x.Link
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
