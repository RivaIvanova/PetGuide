namespace PetGuide.Services.Data
{
    using System.Linq;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;

    public class LocationService : ILocationService
    {
        private readonly IDeletableEntityRepository<Location> locationsRepository;

        public LocationService(IDeletableEntityRepository<Location> locationsRepository)
        {
            this.locationsRepository = locationsRepository;
        }

        public Location GetLocation(District district, string street, string additionalLocationInfo)
        {
            var location = this.locationsRepository
               .All()
               .FirstOrDefault(
               x => x.District == district && x.Street == street && x.AdditionalLocationInfo == additionalLocationInfo);

            if (location == null)
            {
                location = new Location
                {
                    District = district,
                    Street = street,
                    AdditionalLocationInfo = additionalLocationInfo == null ? "No additional location info" : additionalLocationInfo.Trim(),
                };
            }

            return location;
        }
    }
}
