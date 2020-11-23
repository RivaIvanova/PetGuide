namespace PetGuide.Services.Data.Pets
{
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Pets;

    public class AddPetService : IAddPetService
    {
        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly IDeletableEntityRepository<Location> locationsRepository;

        public AddPetService(
            IDeletableEntityRepository<Pet> petsRepository,
            IDeletableEntityRepository<Location> locationsRepository)
        {
            this.petsRepository = petsRepository;
            this.locationsRepository = locationsRepository;
        }

        public async Task AddAsync(AddPetInputModel input)
        {
            var pet = new Pet
            {
                Name = input.Name.Trim(),
                Age = input.Age,
                Description = input.Description.Trim(),
                Type = input.Type,
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
    }
}
