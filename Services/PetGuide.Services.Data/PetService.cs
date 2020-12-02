namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Services.Data.DTOs;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Pets;

    public class PetService : IPetService
    {
        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly IDeletableEntityRepository<Location> locationsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public PetService(
            IDeletableEntityRepository<Pet> petsRepository,
            IDeletableEntityRepository<Location> locationsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.petsRepository = petsRepository;
            this.locationsRepository = locationsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task AddAsync(AddPetInputModel input, string userId)
        {
            var pet = new Pet
            {
                Name = input.Name.Trim(),
                Age = input.Age,
                Description = input.Description.Trim(),
                Type = input.Type,
                UserId = userId,
            };

            var location = this.locationsRepository
                .All()
                .FirstOrDefault(
                x => x.District == input.District && x.Street == input.Street && x.AdditionalInfo == input.AdditionalInfo);

            if (location == null)
            {
                location = new Location
                {
                    District = input.District,
                    Street = input.Street,
                    AdditionalInfo = input.AdditionalInfo == null ? "No additional location info" : input.AdditionalInfo.Trim(),
                };
            }

            pet.Location = location;

            await this.petsRepository.AddAsync(pet);
            await this.petsRepository.SaveChangesAsync();
        }

        public IEnumerable<AllPetsViewModel> GetAll(int page, int petsPerPage)
        {
            if (page <= 0)
            {
                page = 1;
            }

            var petsToSkip = (page - 1) * petsPerPage;

            return this.petsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip(petsToSkip)
                .Take(petsPerPage)
                .To<AllPetsViewModel>()
                .ToList();
        }

        public PetsDetailsDto GetPetsDetails(string id)
        {
            var pet = this.petsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            var user = this.usersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == pet.UserId);

            // TODO: Location null reference
            var location = this.locationsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == pet.Location.Id);

            var data = new PetsDetailsDto
            {
                Name = pet.Name,
                Age = pet.Age,
                Location = location,
                CreatedOn = pet.CreatedOn,
                Description = pet.Description,
                Contact = user,
            };

            return data;
        }

        public int GetCount()
        {
            return this.petsRepository.All().Count();
        }
    }
}
