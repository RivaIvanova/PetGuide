namespace PetGuide.Services.Data
{
    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;

    public interface ILocationService
    {
        Location GetLocation(District district, string street, string additionalLocationInfo);
    }
}
