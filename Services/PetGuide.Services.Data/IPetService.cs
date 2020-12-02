namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Services.Data.DTOs;
    using PetGuide.Web.ViewModels.Pets;

    public interface IPetService
    {
        Task AddAsync(AddPetInputModel input, string userId);

        IEnumerable<AllPetsViewModel> GetAll(int page, int petsPerPage = 12);

        PetsDetailsDto GetPetsDetails(string id);

        int GetCount();
    }
}
