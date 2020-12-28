namespace PetGuide.Web.ViewModels.Pets
{
    using System.Linq;

    using AutoMapper;
    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Pictures;

    public class SearchPetResultViewModel : IMapFrom<Pet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public PetType Type { get; set; }

        public PetStatus Status { get; set; }

        public PictureViewModel FirstPicture { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, SearchPetResultViewModel>()
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
