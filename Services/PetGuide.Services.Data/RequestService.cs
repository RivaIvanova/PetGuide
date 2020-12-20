namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Administration.Dashboard;
    using PetGuide.Web.ViewModels.Requests;

    public class RequestService : IRequestService
    {
        private readonly IDeletableEntityRepository<PetEvent> eventsRepository;
        private readonly IDeletableEntityRepository<Request> requestsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Shelter> sheltersRepository;
        private readonly IEventService eventService;
        private readonly IShelterService shelterService;

        public RequestService(
            IDeletableEntityRepository<PetEvent> eventsRepository,
            IDeletableEntityRepository<Request> requestsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IRepository<Shelter> sheltersRepository,
            IEventService eventService,
            IShelterService shelterService)
        {
            this.eventsRepository = eventsRepository;
            this.requestsRepository = requestsRepository;
            this.usersRepository = usersRepository;
            this.sheltersRepository = sheltersRepository;
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

        // Get All Requests
        public IEnumerable<RequestViewModel> GetAll()
        {
            return this.requestsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .To<RequestViewModel>()
                .ToList();
        }

        // Approve Request
        public async Task ApproveAsync(string id)
        {
            var request = this.requestsRepository.All().FirstOrDefault(x => x.Id == id);
            var volunteer = this.usersRepository.All().FirstOrDefault(x => x.Id == request.VolunteerId);
            var petEvent = this.eventsRepository.All().FirstOrDefault(x => x.Id == request.EventId);
            var shelter = this.sheltersRepository.All().FirstOrDefault(x => x.Id == request.ShelterId);

            if (petEvent != null)
            {
                petEvent.EventVolunteers.Add(new UserPetEvent { PetEventId = petEvent.Id, UserId = volunteer.Id });
                await this.eventsRepository.SaveChangesAsync();
            }

            if (shelter != null)
            {
                shelter.ShelterVolunteers.Add(new UserShelter { ShelterId = shelter.Id, UserId = volunteer.Id });
                await this.sheltersRepository.SaveChangesAsync();
            }

            this.requestsRepository.Delete(request);
            await this.requestsRepository.SaveChangesAsync();
        }

        // Decline Request
        public async Task DeclineAsync(string id)
        {
            var request = this.requestsRepository.All().FirstOrDefault(x => x.Id == id);
            this.requestsRepository.Delete(request);
            await this.requestsRepository.SaveChangesAsync();
        }

        public Request GetRequestById(string id)
        {
            return this.requestsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public EmailViewModel GetEventEmailDetails(string id, string receiverId)
        {
            var receiver = this.usersRepository.All().FirstOrDefault(x => x.Id == receiverId);
            var petEvent = this.eventService.GetEventById(id);

            var html = new StringBuilder();
            html.AppendLine($"<h1>{petEvent.Name}</h1>");
            html.AppendLine($"<h3>{petEvent.Purpose}</h3>");
            html.AppendLine($"<img src=\"https://s3.amazonaws.com/cdn-origin-etr.akc.org/wp-content/uploads/2018/03/13114640/golden-retriever-birthday-party-header.jpg\" />");

            var email = new EmailViewModel
            {
                SenderEmail = "ivanova.riva@gmail.com",
                SenderName = "PetGuide",
                ReceiverEmail = receiver.Email,
                Title = petEvent.Name,
                Content = html.ToString(),
            };

            return email;
        }

        public EmailViewModel GetShelterEmailDetails(string id, string receiverId)
        {
            throw new System.NotImplementedException();
        }
    }
}
