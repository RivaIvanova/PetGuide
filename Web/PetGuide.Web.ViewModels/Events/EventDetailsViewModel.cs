namespace PetGuide.Web.ViewModels.Events
{
    public class EventDetailsViewModel : BaseEventViewModel
    {
        public string Id { get; set; }

        public string Activities { get; set; }

        public string LocationString => $"{this.Location.Street}, {this.Location.District}";

        public bool IsRequestSent { get; set; }
    }
}
