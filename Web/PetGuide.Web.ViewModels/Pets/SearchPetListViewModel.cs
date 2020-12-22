namespace PetGuide.Web.ViewModels.Pets
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Models.Enums;
    using PetGuide.Web.ViewModels.Locations;

    public class SearchPetListViewModel
    {
        public int? Age { get; set; }

        public District? District { get; set; }

        public string Street { get; set; }

        [RegularExpression(@"^(?:[a-zA-z]{2,}\s?){0,10}$", ErrorMessage = "Key words cannot be more than 10, should contain only letters and be separated by whitespace.")]
        public string Description { get; set; }

        public PetType? Type { get; set; }

        public PetStatus? Status { get; set; }

        public PetSize? Size { get; set; }

        public PetColor? Color { get; set; }

        public IEnumerable<SearchPetResultViewModel> Pets { get; set; }

        public IEnumerable<SearchPetLocationViewModel> Locations { get; set; }
    }
}
