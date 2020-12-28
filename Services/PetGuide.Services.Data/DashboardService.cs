namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Administration.Dashboard;
    using PetGuide.Web.ViewModels.Administration.Events;
    using PetGuide.Web.ViewModels.Administration.Pets;
    using PetGuide.Web.ViewModels.Administration.Shelters;

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

        // Events Admin View
        public IEnumerable<EventViewModel> GetAllEventsAdminView()
        {
            return this.eventsRepository
                 .AllAsNoTracking()
                 .OrderByDescending(x => x.DateTime)
                 .To<EventViewModel>()
                 .ToList();
        }

        public EventsListViewModel EventsAdminView()
        {
            var volunteersCount = this.usersRepository.AllAsNoTracking().Where(x => x.PetEvents.Count > 0).Count();
            var eventsCount = this.eventsRepository.AllAsNoTracking().Count();
            var events = this.GetAllEventsAdminView();

            var petEvent = new EventsListViewModel
            {
                VolunteersCount = volunteersCount,
                EventsCount = eventsCount,
                Events = events,
            };

            return petEvent;
        }

        // Shelters Admin View
        public IEnumerable<ShelterViewModel> GetAllSheltersAdminView()
        {
            return this.sheltersRepository
                 .AllAsNoTracking()
                 .OrderByDescending(x => x.CreatedOn)
                 .To<ShelterViewModel>()
                 .ToList();
        }

        public SheltersListViewModel SheltersAdminView()
        {
            var petsCount = this.petsRepository.AllAsNoTracking().Where(x => x.ShelterId != null).Count();
            var volunteersCount = this.usersRepository.AllAsNoTracking().Where(x => x.Shelters.Count > 0).Count();
            var sheltersCount = this.sheltersRepository.AllAsNoTracking().Count();
            var shelters = this.GetAllSheltersAdminView();

            var shelter = new SheltersListViewModel
            {
                PetsCount = petsCount,
                VolunteersCount = volunteersCount,
                SheltersCount = sheltersCount,
                Shelters = shelters,
            };

            return shelter;
        }

        // Pets Admin View
        public IEnumerable<PetsViewModel> GetAllPetsAdminView()
        {
            return this.petsRepository
                 .AllAsNoTracking()
                 .OrderByDescending(x => x.CreatedOn)
                 .To<PetsViewModel>()
                 .ToList();
        }

        public PetsListViewModel PetsAdminView()
        {
            var lostPetsCount = this.petsRepository.AllAsNoTracking().Where(x => x.Status.Equals(PetStatus.Lost)).Count();
            var foundPetsCount = this.petsRepository.AllAsNoTracking().Where(x => x.Status.Equals(PetStatus.Found)).Count();
            var spottedPetsCount = this.petsRepository.AllAsNoTracking().Where(x => x.Status.Equals(PetStatus.Spotted)).Count();
            var shelteredPetsCount = this.petsRepository.AllAsNoTracking().Where(x => x.Status.Equals(PetStatus.Sheltered)).Count();

            var pets = this.GetAllPetsAdminView();

            var pet = new PetsListViewModel
            {
                LostPetsCount = lostPetsCount,
                FoundPetsCount = foundPetsCount,
                SpottedPetsCount = spottedPetsCount,
                ShelteredPetsCount = shelteredPetsCount,
                Pets = pets,
            };
            return pet;
        }
    }
}
