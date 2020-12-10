namespace PetGuide.Web.ViewModels.Shelters
{
    using PetGuide.Data.Models;

    public class AllSheltersViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Picture Picture { get; set; }

        public Location Location { get; set; }

        public string Address => $"{this.Location.District} {this.Location.Street} {this.Location.AdditionalLocationInfo}";
    }
}
