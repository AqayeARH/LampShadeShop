using Lampshade.Query.Contracts.Slider;
using ShopManagement.Infra.EfCore;

namespace Lampshade.Query.Queries
{
    public class SliderQuery : ISliderQuery
    {
        #region Constractor Injection

        private readonly ShopContext _shopContext;
        public SliderQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        #endregion
        public List<SliderQueryModel> ShowSliders()
        {
            return _shopContext.Sliders.Where(x => x.IsRemoved == false)
                .Select(x => new SliderQueryModel
                {
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Heading = x.Heading,
                    Title = x.Title,
                    Text = x.Text,
                    BtnText = x.BtnText,
                    Link = x.Link
                }).ToList();
        }
    }
}
