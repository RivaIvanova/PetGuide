namespace PetGuide.Services.Data.Pets
{
    using System.Linq;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Services.Data.DTOs;

    public class GetPetsDetailsService : IGetPetsDetailsService
    {
        private readonly IDeletableEntityRepository<Pet> petsRepository;

        public GetPetsDetailsService(
            IDeletableEntityRepository<Pet> petsRepository)
        {
            this.petsRepository = petsRepository;
        }

        public PetsDetailsDto GetPetsDetails(string id)
        {
            var pet = this.petsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);

            var data = new PetsDetailsDto
            {
                Name = pet.Name,
                Age = pet.Age,
                Location = pet.Location,
                CreatedOn = pet.CreatedOn,
                Description = pet.Description,
                Contact = pet.User,
            };

            return data;
        }
    }
}
