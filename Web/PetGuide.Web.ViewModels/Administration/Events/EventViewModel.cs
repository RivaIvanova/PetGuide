namespace PetGuide.Web.ViewModels.Administration.Events
{
    using System;

    using PetGuide.Data.Models;
    using PetGuide.Services.Mapping;

    public class EventViewModel : IMapFrom<PetEvent>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime DateTime { get; set; }

        public Location Location { get; set; }

        public string LocationAsString => $"{this.Location.Street}, {this.Location.District}";

        public int EventVolunteersCount { get; set; }
    }
}
