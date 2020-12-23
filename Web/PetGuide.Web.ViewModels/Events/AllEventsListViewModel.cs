namespace PetGuide.Web.ViewModels.Events
{
    using System.Collections.Generic;

    public class AllEventsListViewModel
    {
        public IEnumerable<AllEventsViewModel> TodaysEvents { get; set; }

        public IEnumerable<AllEventsViewModel> UpcommingEvents { get; set; }

        public IEnumerable<AllEventsViewModel> PastEvents { get; set; }

        public IEnumerable<AllEventsViewModel> VolunteerEvents { get; set; }
    }
}
