namespace PetGuide.Web.ViewModels.Locations
{
    using System.Linq;

    using AutoMapper;
    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Pets;
    using PetGuide.Web.ViewModels.Pictures;

    public class SearchPetLocationViewModel : SearchPetResultViewModel, IMapFrom<Pet>, IHaveCustomMappings
    {
        public string Street { get; set; }

        public District District { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string LatitudeAsString => this.Latitude.ToString().Replace(',', '.');

        public string LongitudeAsString => this.Longitude.ToString().Replace(',', '.');

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, SearchPetLocationViewModel>()
               .ForMember(x => x.Street, opt => opt.MapFrom(x => x.Location.Street))
               .ForMember(x => x.District, opt => opt.MapFrom(x => x.Location.District))
               .ForMember(x => x.Latitude, opt => opt.MapFrom(x => x.Location.Latitude))
               .ForMember(x => x.Longitude, opt => opt.MapFrom(x => x.Location.Longitude))
               .ForMember(x => x.FirstPicture, opt => opt.MapFrom(
                    x => x.Pictures
                    .Select(x => new PictureViewModel
                    {
                        Id = x.Id,
                        Name = x.Folder + "/Thumbnail_" + x.Id + ".jpg",
                    })
                    .FirstOrDefault()));
        }
    }
}
