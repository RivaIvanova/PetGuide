namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Shelters;

    public class ShelterService : IShelterService
    {
        private readonly IRepository<Shelter> sheltersRepository;
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Location> locationsRepository;
        private readonly IRepository<Pet> petsRepository;
        private readonly ILocationService locationService;

        public ShelterService(
            IRepository<Shelter> sheltersRepository,
            IRepository<ApplicationUser> usersRepository,
            IRepository<Location> locationsRepository,
            IRepository<Pet> petsRepository,
            ILocationService locationService)
        {
            this.sheltersRepository = sheltersRepository;
            this.usersRepository = usersRepository;
            this.locationsRepository = locationsRepository;
            this.petsRepository = petsRepository;
            this.locationService = locationService;
        }

        // Add Shelter
        public async Task AddAsync(ShelterInputModel input)
        {
            var location = this.locationService.GetLocation(input.Location.District, input.Location.Street, input.Location.AdditionalLocationInfo);

            var shelter = new Shelter
            {
                Name = input.Name.Trim(),
                Description = input.Description.Trim(),
                Activities = input.Activities.Trim(),
                Location = location,
            };

            await this.sheltersRepository.AddAsync(shelter);
            await this.sheltersRepository.SaveChangesAsync();
        }

        // Edit Shelter
        public async Task EditAsync(string id, ShelterInputModel input)
        {
            var shelter = this.GetShelterById(id);
            var location = this.locationService.GetLocation(input.Location.District, input.Location.Street, input.Location.AdditionalLocationInfo);

            shelter.Name = input.Name;
            shelter.Description = input.Description;
            shelter.Activities = input.Activities;
            shelter.Location = location;

            await this.sheltersRepository.SaveChangesAsync();
        }

        // Delete Shelter
        public async Task DeleteAsync(string id)
        {
            var shelter = this.GetShelterById(id);
            this.sheltersRepository.Delete(shelter);
            await this.sheltersRepository.SaveChangesAsync();
        }

        // Get All Shelters
        public ICollection<AllSheltersViewModel> GetAll()
        {
            return this.sheltersRepository
                .AllAsNoTracking()
               .Select(x => new AllSheltersViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Picture = x.Pictures.FirstOrDefault(),
                   Location = x.Location,
               })
               .ToList();
        }

        // Get Shelter Edit View
        public ShelterInputModel GetShelterEdit(string id)
        {
            var shelter = this.GetShelterById(id);
            var volunteers = this.usersRepository.All().Where(x => x.Shelters.Any(x => x.ShelterId == id)).ToList();
            var location = this.locationsRepository.All().FirstOrDefault(x => x.Id == shelter.LocationId);

            var viewModel = new ShelterInputModel
            {
                Name = shelter.Name,
                Location = location,
                Description = shelter.Description,
                Activities = shelter.Activities,
            };

            return viewModel;
        }

        // Get Shelter By Id
        public Shelter GetShelterById(string id)
        {
            return this.sheltersRepository.All().FirstOrDefault(x => x.Id == id);
        }

        // Shelter Details
        public ShelterDetailsViewModel GetSheltesDetails(string id)
        {
            var shelter = this.sheltersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            var location = this.locationsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == shelter.LocationId);
            var pets = this.petsRepository.AllAsNoTracking().Where(x => x.ShelterId == shelter.Id).ToList();
            var volunteers = this.usersRepository.AllAsNoTracking().Where(x => x.Shelters.Any(x => x.ShelterId == shelter.Id)).ToList();

            var viewModel = new ShelterDetailsViewModel
            {
                Id = shelter.Id,
                Name = shelter.Name,
                Description = shelter.Description,
                Activities = shelter.Activities,
                Location = location,
                Pets = pets,
                ShelterVolunteers = volunteers,
            };

            return viewModel;
        }

        // Add Pet To Shelter
        public async Task AddAsync(string shelterId, AddPetToShelterInputModel input, string userId)
        {
            var shelter = this.sheltersRepository.All().FirstOrDefault(x => x.Id == shelterId);

            var pet = new Pet
            {
                Name = input.Name.Trim(),
                Type = input.Type,
                Color = input.Color,
                Age = input.Age,
                Size = input.Size,
                Status = input.Status,
                Description = input.Description.Trim(),
                LocationId = shelter.LocationId,
                ShelterId = shelter.Id,
                UserId = userId,
            };

            await this.petsRepository.AddAsync(pet);
            await this.petsRepository.SaveChangesAsync();
        }
    }
}
