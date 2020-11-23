namespace PetGuide.Services.Data.Pets
{
    using System.Linq;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Services.Data.DTOs;

    public class GetPetsDetailsService : IGetPetsDetailsService
    {
        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly IDeletableEntityRepository<Location> locationsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public GetPetsDetailsService(
            IDeletableEntityRepository<Pet> petsRepository,
            IDeletableEntityRepository<Location> locationsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.petsRepository = petsRepository;
            this.locationsRepository = locationsRepository;
            this.usersRepository = usersRepository;
        }

        public PetsDetailsDto GetPetsDetails()
        {
            var data = new PetsDetailsDto
            {
                Name = this.petsRepository.All().FirstOrDefault().Name,
                Location = this.locationsRepository.All().FirstOrDefault(),
                Age = this.petsRepository.All().FirstOrDefault().Age,
                Description = this.petsRepository.All().FirstOrDefault().Description,
                Contact = this.usersRepository.All().FirstOrDefault(),
            };

            return data;
        }
    }
}
