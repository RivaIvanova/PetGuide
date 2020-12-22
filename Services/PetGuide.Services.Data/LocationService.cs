namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using GoogleMaps.LocationServices;
    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Locations;

    public class LocationService : ILocationService
    {
        private readonly IDeletableEntityRepository<Location> locationsRepository;
        private readonly IDeletableEntityRepository<Pet> petsRepository;

        public LocationService(
            IDeletableEntityRepository<Location> locationsRepository,
            IDeletableEntityRepository<Pet> petsRepository)
        {
            this.locationsRepository = locationsRepository;
            this.petsRepository = petsRepository;
        }

        public Location GetLocation(District district, string street, string additionalLocationInfo)
        {
            var location = this.locationsRepository
               .All()
               .FirstOrDefault(
               x => x.District == district && x.Street == street && x.AdditionalLocationInfo == additionalLocationInfo);

            if (location == null)
            {
                var gls = new GoogleLocationService("AIzaSyBCCwf0E4iP_WGafh8B-UmggGiCoRXalWc");

                var address = $"{street}, {district}, Sofia";

                var latlong = gls.GetLatLongFromAddress(address);
                var latitude = latlong.Latitude;
                var longitude = latlong.Longitude;

                location = new Location
                {
                    District = district,
                    Street = street.Trim(),
                    AdditionalLocationInfo = additionalLocationInfo == null ? "No additional location info" : additionalLocationInfo.Trim(),
                    Latitude = latitude,
                    Longitude = longitude,
                };
            }

            return location;
        }

        public IEnumerable<SearchPetLocationViewModel> GetPetsLocation()
        {
            return this.petsRepository
                .AllAsNoTracking()
                .To<SearchPetLocationViewModel>()
                .ToList();
        }
    }
}
