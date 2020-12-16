namespace PetGuide.Web.ViewModels.Events
{
    using PetGuide.Data.Models;

    public class EventDetailsViewModel : BaseEventViewModel
    {
        public string Id { get; set; }

        public string Activities { get; set; }
    }
}
