namespace PetGuide.Web.ViewModels.Pets
{
    using System;
    using System.Linq;

    using AutoMapper;
    using PetGuide.Data.Models;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Pictures;

    public class AllPetsViewModel : IMapFrom<Pet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("f");

        public string Description { get; set; }

        public string ShortDescription => this.Description.Length >= 100 ? $"{this.Description.Substring(0, 100)}..." : this.Description;

        public ApplicationUser Contact { get; set; }

        public PictureViewModel FirstPicture { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, AllPetsViewModel>()
                .ForMember(x => x.Contact, opt => opt.MapFrom(x => x.User))
                .ForMember(x => x.FirstPicture, opt => opt.MapFrom(
                    x => x.Pictures
                    .Select(x => new PictureViewModel
                    {
                        Id = x.Id,
                        Name = x.Folder + "/Fullscreen_" + x.Id + ".jpg",
                    })
                    .FirstOrDefault()));
        }
    }
}
