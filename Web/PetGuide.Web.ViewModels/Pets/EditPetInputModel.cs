namespace PetGuide.Web.ViewModels.Pets
{
    using System;

    public class EditPetInputModel : BasePetViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("f");
    }
}
