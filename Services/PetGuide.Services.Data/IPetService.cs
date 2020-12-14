﻿namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Pets;

    public interface IPetService
    {
        Task AddAsync(AddPetInputModel input, string userId, string shelterId = null);

        Task EditAsync(string id, EditPetInputModel input);

        Task DeleteAsync(string id);

        IEnumerable<AllPetsViewModel> GetAll(int page, int petsPerPage = 12);

        IEnumerable<SearchPetResultViewModel> GetRecentlyAdded();

        PetsDetailsViewModel GetPetsDetails(string id);

        EditPetInputModel GetPetEdit(string id);

        IEnumerable<SearchPetResultViewModel> SearchPets(SearchPetListViewModel input);

        SearchPetListViewModel SetSearchValues(SearchPetListViewModel input);

        Pet GetPetById(string id);

        int GetCount();
    }
}
