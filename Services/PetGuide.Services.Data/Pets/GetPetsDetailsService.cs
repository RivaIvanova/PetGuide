namespace PetGuide.Services.Data.Pets
{
    using System.Linq;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Services.Data.DTOs;

    public class GetPetsDetailsService : IGetPetsDetailsService
    {
        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Location> locationsRepository;

        public GetPetsDetailsService(
            IDeletableEntityRepository<Pet> petsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Location> locationsRepository)
        {
            this.petsRepository = petsRepository;
            this.usersRepository = usersRepository;
            this.locationsRepository = locationsRepository;
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
    }
}
