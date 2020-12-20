namespace PetGuide.Web.ViewModels.Administration.Shelters
{
    using System;

    using PetGuide.Data.Models;
    using PetGuide.Services.Mapping;

    public class ShelterViewModel : IMapFrom<Shelter>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public Location Location { get; set; }

        public string LocationAsString => $"{this.Location.Street}, {this.Location.District}";

        public int ShelterVolunteersCount { get; set; }

        public int PetsCount { get; set; }
    }
}
