namespace PetGuide.Web.ViewModels.Administration.Pets
{
    using System.Collections.Generic;

    public class PetsListViewModel
    {
        public int LostPetsCount { get; set; }

        public int FoundPetsCount { get; set; }

        public int SpottedPetsCount { get; set; }

        public int ShelteredPetsCount { get; set; }

        public IEnumerable<PetsViewModel> Pets { get; set; }
    }
}
