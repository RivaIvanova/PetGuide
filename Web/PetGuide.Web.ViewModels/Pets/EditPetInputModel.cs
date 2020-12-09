namespace PetGuide.Web.ViewModels.Pets
{
    using System;

    using PetGuide.Data.Models;

    public class EditPetInputModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public int? Age { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("f");

        public string Description { get; set; }

        public PetType Type { get; set; }
    }
}
