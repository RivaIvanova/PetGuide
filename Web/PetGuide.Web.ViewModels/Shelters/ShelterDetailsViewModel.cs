namespace PetGuide.Web.ViewModels.Shelters
{
    using System.Collections.Generic;

    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Pets;
    using PetGuide.Web.ViewModels.Pictures;

    public class ShelterDetailsViewModel : BaseShelterViewModel
    {
        public string Id { get; set; }

        public IEnumerable<SearchPetResultViewModel> Pets { get; set; }

        public IEnumerable<ApplicationUser> ShelterVolunteers { get; set; }

        public PictureViewModel FirstPictureToShow { get; set; }

        public IEnumerable<PictureViewModel> PicturesToShow { get; set; }
    }
}
