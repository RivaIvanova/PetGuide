namespace PetGuide.Web.ViewModels.Events
{
    using System;

    using PetGuide.Data.Models;

    public class BaseEventViewModel
    {
        public string Name { get; set; }

        public string Purpose { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string Description { get; set; }

        public Location Location { get; set; }
    }
}
