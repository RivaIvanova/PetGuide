namespace PetGuide.Web.ViewModels.Administration.Pets
{
    using System;

    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;
    using PetGuide.Services.Mapping;

    public class PetsViewModel : IMapFrom<Pet>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public PetType Type { get; set; }

        public PetStatus Status { get; set; }

        public PetSize Size { get; set; }

        public PetColor Color { get; set; }

        public DateTime CreatedOn { get; set; }

        public Location Location { get; set; }

        public string LocationAsString => $"{this.Location.Street}, {this.Location.District}";
    }
}
