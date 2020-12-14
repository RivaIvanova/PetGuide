namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Shelters;

    public interface IShelterService
    {
        Task AddAsync(ShelterInputModel input);

        Task EditAsync(string id, ShelterInputModel input);

        Task DeleteAsync(string id);

        Task AddAsync(string shelterId, AddPetToShelterInputModel input, string userId);

        ShelterInputModel GetShelterEdit(string id);

        ShelterDetailsViewModel GetSheltesDetails(string id);

        Shelter GetShelterById(string id);

        ICollection<AllSheltersViewModel> GetAll();
    }
}
