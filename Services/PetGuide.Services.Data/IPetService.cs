namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Pets;

    public interface IPetService
    {
        Task AddAsync(AddPetInputModel input, string userId);

        Task EditAsync(string id, EditPetInputModel input);

        Task DeleteAsync(string id);

        IEnumerable<AllPetsViewModel> GetAll(int page, int petsPerPage = 12);

        PetsDetailsViewModel GetPetsDetails(string id);

        EditPetInputModel GetPetEdit(string id);

        Pet GetPetById(string id);

        int GetCount();
    }
}
