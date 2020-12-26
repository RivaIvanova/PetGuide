namespace PetGuide.Web.ViewModels.Pets
{
    using System;
    using System.Collections.Generic;

    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Pictures;

    public class PetsDetailsViewModel : BasePetViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("f");

        public ApplicationUser Contact { get; set; }

        public PictureViewModel FirstPictureToShow { get; set; }

        public IEnumerable<PictureViewModel> PicturesToShow { get; set; }
    }
}
