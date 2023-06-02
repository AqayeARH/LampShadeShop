using _0.Framework.Domain;

namespace ShopManagement.Domain.SliderAgg
{
    public class Slider : BaseEntity
    {
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Heading { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string BtnText { get; private set; }
        public string Link { get; private set; }
        public bool IsRemoved { get; private set; }

        //The constructor is called when creating a new instance
        public Slider(string picture, string pictureAlt, string pictureTitle, string heading, string title, string text, string btnText, string link)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link = link;
            IsRemoved = false;
        }

        //The edit method is called when an entity is changed
        public void Edit(string picture, string pictureAlt, string pictureTitle, string heading, string title, string text, string btnText, string link)
        {
            if (!string.IsNullOrEmpty(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link = link;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
