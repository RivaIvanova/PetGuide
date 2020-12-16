namespace PetGuide.Web.ViewModels.Events
{
    using PetGuide.Data.Models;

    public class AllEventsViewModel : BaseEventViewModel
    {
        public string Id { get; set; }

        public string ShortDescription => this.Description.Length >= 400 ? $"{this.Description.Substring(0, 400)}..." : this.Description;

        public string LocationAsString => $"{this.Location.Street}, {this.Location.District},  {this.Location.AdditionalLocationInfo}";

    }
}
