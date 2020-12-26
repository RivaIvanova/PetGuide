namespace PetGuide.Web.ViewModels.Shelters
{
    using System.Collections.Generic;

    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Pictures;

    public class AllSheltersViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public string Address => $"{this.Location.District} {this.Location.Street} {this.Location.AdditionalLocationInfo}";

        public IEnumerable<PictureViewModel> PicturesToShow { get; set; }
    }
}
