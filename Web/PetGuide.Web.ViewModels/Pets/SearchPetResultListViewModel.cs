namespace PetGuide.Web.ViewModels.Pets
{
    using System.Collections.Generic;

    using PetGuide.Web.ViewModels.Locations;

    public class SearchPetResultListViewModel
    {
        public IEnumerable<SearchPetLocationViewModel> PetsWithLocation { get; set; }
    }
}
