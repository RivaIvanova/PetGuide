namespace PetGuide.Services.Data
{
    using PetGuide.Data.Models;

    public interface ILocationService
    {
        Location GetLocation(District district, string street, string additionalLocationInfo);
    }
}
