namespace PetGuide.Services.Data
{
    using System;
    using System.Linq;
    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;

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
               x => x.District == district && x.Street == street && x.AdditionalInfo == additionalLocationInfo);

            if (location == null)
            {
                location = new Location
                {
                    District = district,
                    Street = street,
                    AdditionalInfo = additionalLocationInfo == null ? "No additional location info" : additionalLocationInfo.Trim(),
                };
            }

            return location;
        }
    }
}
