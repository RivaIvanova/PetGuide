namespace PetGuide.Web.ViewModels.Pets
{
    using System;

    using AutoMapper;
    using PetGuide.Data.Models;
    using PetGuide.Services.Mapping;

    public class AllPetsViewModel : IMapFrom<Pet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("f");

        public string Description { get; set; }

        public string ShortDescription => this.Description.Length >= 100 ? $"{this.Description.Substring(0, 100)}..." : this.Description;

        public ApplicationUser Contact { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, AllPetsViewModel>()
                .ForMember(x => x.Contact, opt => opt.MapFrom(x => x.User));
        }
    }
}
