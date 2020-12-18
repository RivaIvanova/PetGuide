namespace PetGuide.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int PetsCount { get; set; }

        public int UsersCount { get; set; }

        public int EventsCount { get; set; }

        public int SheltersCount { get; set; }

        public IEnumerable<RequestViewModel> Requests { get; set; }
    }
}
