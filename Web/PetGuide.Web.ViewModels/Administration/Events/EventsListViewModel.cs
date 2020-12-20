namespace PetGuide.Web.ViewModels.Administration.Events
{
    using System.Collections.Generic;

    public class EventsListViewModel
    {
        public int VolunteersCount { get; set; }

        public int EventsCount { get; set; }

        public IEnumerable<EventViewModel> Events { get; set; }
    }
}
