namespace PetGuide.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;

    public class RequestService : IRequestService
    {
        private readonly IDeletableEntityRepository<PetEvent> eventsRepository;
        private readonly IDeletableEntityRepository<Request> requestsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IEventService eventService;
        private readonly IShelterService shelterService;

        public RequestService(
            IDeletableEntityRepository<PetEvent> eventsRepository,
            IDeletableEntityRepository<Request> requestsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IEventService eventService,
            IShelterService shelterService)
        {
            this.eventsRepository = eventsRepository;
            this.requestsRepository = requestsRepository;
            this.usersRepository = usersRepository;
            this.eventService = eventService;
            this.shelterService = shelterService;
        }

        // Add Request
        public async Task AddAsync(string id, string volunteerId)
        {
            var volunteer = this.usersRepository.All().FirstOrDefault(x => x.Id == volunteerId);
            var petEvent = this.eventService.GetEventById(id);
            var shelter = this.shelterService.GetShelterById(id);

            var request = new Request
            {
                DateTime = DateTime.UtcNow,
                Volunteer = volunteer,
            };

            if (petEvent != null)
            {
                request.Event = petEvent;
            }

            if (shelter != null)
            {
                request.Shelter = shelter;
            }

            await this.requestsRepository.AddAsync(request);
            await this.requestsRepository.SaveChangesAsync();
        }

        // Is Volunteer Request Alreday Sent
        public bool IsRequestSent(string id, string volunteerId)
        {
            return this.requestsRepository.AllAsNoTracking()
                .Any(x => (x.EventId == id && x.VolunteerId == volunteerId) 
                || (x.ShelterId == id && x.VolunteerId == volunteerId));
        }
    }
}
