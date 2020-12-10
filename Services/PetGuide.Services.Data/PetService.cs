﻿namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Pets;

    public class PetService : IPetService
    {
        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly IDeletableEntityRepository<Location> locationsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly ILocationService locationService;

        public PetService(
            IDeletableEntityRepository<Pet> petsRepository,
            IDeletableEntityRepository<Location> locationsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            ILocationService locationService)
        {
            this.petsRepository = petsRepository;
            this.locationsRepository = locationsRepository;
            this.usersRepository = usersRepository;
            this.locationService = locationService;
        }

        // Add Pet
        public async Task AddAsync(AddPetInputModel input, string userId)
        {
            var pet = new Pet
            {
                Name = input.Name.Trim(),
                Type = input.Type,
                Color = input.Color,
                Age = input.Age,
                Size = input.Size,
                Status = input.Status,
                Description = input.Description.Trim(),
                UserId = userId,
            };

            var location = this.locationService.GetLocation(input.Location.District, input.Location.Street, input.Location.AdditionalLocationInfo);

            pet.Location = location;

            await this.petsRepository.AddAsync(pet);
            await this.petsRepository.SaveChangesAsync();
        }

        // Get Pet Details View
        public PetsDetailsViewModel GetPetsDetails(string id)
        {
            var pet = this.petsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            var user = this.usersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == pet.UserId);
            var location = this.locationsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == pet.LocationId);

            var viewModel = new PetsDetailsViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                Type = pet.Type,
                Color = pet.Color,
                Age = pet.Age,
                Size = pet.Size,
                Location = location,
                Status = pet.Status,
                CreatedOn = pet.CreatedOn,
                Description = pet.Description,
                Contact = user,
            };

            return viewModel;
        }

        // Get Pet Edit View
        public EditPetInputModel GetPetEdit(string id)
        {
            var pet = this.petsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            var location = this.locationsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == pet.LocationId);

            var viewModel = new EditPetInputModel
            {
                Name = pet.Name,
                Type = pet.Type,
                Color = pet.Color,
                Age = pet.Age,
                Size = pet.Size,
                Location = location,
                Status = pet.Status,
                Description = pet.Description,
            };

            return viewModel;
        }

        // Edit Pet
        public async Task EditAsync(string id, EditPetInputModel input)
        {
            var pet = this.petsRepository.All().FirstOrDefault(x => x.Id == id);
            var location = this.locationService.GetLocation(input.Location.District, input.Location.Street, input.Location.AdditionalLocationInfo);

            pet.Name = input.Name;
            pet.Type = input.Type;
            pet.Color = input.Color;
            pet.Age = input.Age;
            pet.Size = input.Size;
            pet.Status = input.Status;
            pet.Description = input.Description;
            pet.Location = location;
            pet.Type = input.Type;

            await this.petsRepository.SaveChangesAsync();
        }

        // Delete Pet
        public async Task DeleteAsync(string id)
        {
            var pet = this.petsRepository.All().FirstOrDefault(x => x.Id == id);
            this.petsRepository.Delete(pet);
            await this.petsRepository.SaveChangesAsync();
        }

        // Get All Pets View
        public IEnumerable<AllPetsViewModel> GetAll(int page, int petsPerPage)
        {
            if (page <= 0)
            {
                page = 1;
            }

            var petsToSkip = (page - 1) * petsPerPage;

            return this.petsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(petsToSkip)
                .Take(petsPerPage)
                .To<AllPetsViewModel>()
                .ToList();
        }

        // Get Pets Count
        public int GetCount()
        {
            return this.petsRepository.All().Count();
        }

        // Get Pet By Id
        public Pet GetPetById(string id)
        {
            return this.petsRepository.All().FirstOrDefault(x => x.Id == id);
        }

        // Get Recently Added Pets
        public IEnumerable<SearchPetViewModel> GetRecentlyAdded()
        {
            return this.petsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Take(10)
                .To<SearchPetViewModel>()
                .ToList();
        }
    }
}
