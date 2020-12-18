namespace PetGuide.Web.ViewModels.Administration.Dashboard
{
    using System;

    using PetGuide.Data.Models;
    using PetGuide.Services.Mapping;

    public class RequestViewModel : IMapFrom<Request>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string VolunteerId { get; set; }

        public ApplicationUser Volunteer { get; set; }

        public string EventId { get; set; }

        public PetEvent Event { get; set; }

        public string ShelterId { get; set; }

        public Shelter Shelter { get; set; }
    }
}
