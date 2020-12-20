namespace PetGuide.Services.Data
{
    using System.Linq;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Administration.Dashboard;

    public class DashboardService : IDashboardService
    {
        private readonly IDeletableEntityRepository<PetEvent> eventsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly IRepository<Shelter> sheltersRepository;
        private readonly IRequestService requestsService;

        public DashboardService(
            IDeletableEntityRepository<PetEvent> eventsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Pet> petsRepository,
            IRepository<Shelter> sheltersRepository,
            IRequestService requestsService)
        {
            this.eventsRepository = eventsRepository;
            this.usersRepository = usersRepository;
            this.petsRepository = petsRepository;
            this.sheltersRepository = sheltersRepository;
            this.requestsService = requestsService;
        }

        // Get Dashboard
        public DashboardViewModel GetDashboard()
        {
            var pets = this.petsRepository.AllAsNoTracking().Count();
            var users = this.usersRepository.AllAsNoTracking().Count();
            var shelters = this.sheltersRepository.AllAsNoTracking().Count();
            var events = this.eventsRepository.AllAsNoTracking().Count();

            var dashboard = new DashboardViewModel
            {
                PetsCount = pets,
                UsersCount = users,
                SheltersCount = shelters,
                EventsCount = events,
                Requests = this.requestsService.GetAll(),
            };

            return dashboard;
        }
    }
}
