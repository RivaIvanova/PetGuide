namespace PetGuide.Services.Data.Shelters
{
    using System.Collections.Generic;
    using System.Linq;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Shelters;


    public class GetAllSheltersService : IGetAllSheltersService
    {
        private readonly IRepository<Shelter> sheltersRepository;

        public GetAllSheltersService(
            IRepository<Shelter> sheltersRepository)
        {
            this.sheltersRepository = sheltersRepository;
        }

        public ICollection<AllSheltersViewModel> GetAll()
        {
            return this.sheltersRepository.AllAsNoTracking()
                .Select(x => new AllSheltersViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Pictures.FirstOrDefault(),
                    Location = x.Location,
                })
                .ToList();
        }
    }
}
