namespace PetGuide.Services.Data.Pets
{
    using System.Collections.Generic;

    using PetGuide.Web.ViewModels.Pets;

    public interface IGetAllPetsService
    {
        ICollection<PetsDetailsViewModel> GetAll();
    }
}
