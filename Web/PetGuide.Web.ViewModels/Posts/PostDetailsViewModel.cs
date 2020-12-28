namespace PetGuide.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;
    using PetGuide.Web.ViewModels.Pictures;

    public class PostDetailsViewModel : BasePostViewModel
    {
        public string Id { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<Tag> AllTags { get; set; }

        public IDictionary<PostCategory, int> AllCategories { get; set; }

        public PictureViewModel FirstPictureToShow { get; set; }

        public IEnumerable<PictureViewModel> PicturesToShow { get; set; }
    }
}
