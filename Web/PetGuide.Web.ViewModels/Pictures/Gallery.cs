namespace PetGuide.Web.ViewModels.Pictures
{
    using System.Collections.Generic;

    public class Gallery
    {
        public IEnumerable<PictureViewModel> PicturesToShow { get; set; }
    }
}
