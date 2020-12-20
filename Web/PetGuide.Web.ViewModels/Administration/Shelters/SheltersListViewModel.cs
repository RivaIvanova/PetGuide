namespace PetGuide.Web.ViewModels.Administration.Shelters
{
    using System.Collections.Generic;

    public class SheltersListViewModel
    {
        public int PetsCount { get; set; }

        public int VolunteersCount { get; set; }

        public int SheltersCount { get; set; }

        public IEnumerable<ShelterViewModel> Shelters { get; set; }
    }
}
