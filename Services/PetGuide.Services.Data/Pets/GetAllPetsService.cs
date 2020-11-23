namespace PetGuide.Services.Data.Pets
{
    using System.Collections.Generic;
    using System.Linq;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Pets;

    public class GetAllPetsService : IGetAllPetsService
    {
        private readonly IDeletableEntityRepository<Pet> petsRepository;

        public GetAllPetsService(
            IDeletableEntityRepository<Pet> petsRepository)
        {
            this.petsRepository = petsRepository;

        }

        public ICollection<PetsDetailsViewModel> GetAll()
        {
            return this.petsRepository.AllAsNoTracking()
                .Select(x => new PetsDetailsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    Contact = x.User,
                })
                .ToList();
        }
    }
}
