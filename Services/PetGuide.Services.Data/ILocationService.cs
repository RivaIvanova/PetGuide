namespace PetGuide.Services.Data
{
    using System.Collections.Generic;

    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;
    using PetGuide.Web.ViewModels.Locations;

    public interface ILocationService
    {
        Location GetLocation(District district, string street, string additionalLocationInfo);

        IEnumerable<SearchPetLocationViewModel> GetAllPetsLocation();
    }
}
