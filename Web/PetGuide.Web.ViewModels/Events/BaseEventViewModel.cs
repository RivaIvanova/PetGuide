namespace PetGuide.Web.ViewModels.Events
{
    using PetGuide.Data.Models;

    public class BaseEventViewModel
    {
        public string Name { get; set; }

        public string Purpose { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Description { get; set; }

        public Location Location { get; set; }
    }
}
