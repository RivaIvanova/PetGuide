namespace PetGuide.Web.ViewModels.Shelters
{
    using System.Collections.Generic;

    using PetGuide.Data.Models;

    public class ShelterDetailsViewModel : BaseShelterViewModel
    {
        public string Id { get; set; }

        public IEnumerable<Pet> Pets { get; set; }

        public IEnumerable<ApplicationUser> ShelterVolunteers { get; set; }
    }
}
