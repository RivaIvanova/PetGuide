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
        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly IDeletableEntityRepository<Subscription> subscriptionsRepository;
        private readonly IRepository<Shelter> sheltersRepository;

        public RequestService(
            IDeletableEntityRepository<PetEvent> eventsRepository,
            IDeletableEntityRepository<Request> requestsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Pet> petsRepository,
            IDeletableEntityRepository<Subscription> subscriptionsRepository,
            IRepository<Shelter> sheltersRepository)
        {
            this.eventsRepository = eventsRepository;
            this.requestsRepository = requestsRepository;
            this.usersRepository = usersRepository;
            this.petsRepository = petsRepository;
            this.subscriptionsRepository = subscriptionsRepository;
            this.sheltersRepository = sheltersRepository;
        }

        // Add Request
        public async Task AddAsync(string id, string volunteerId)
        {
            var volunteer = this.usersRepository.All().FirstOrDefault(x => x.Id == volunteerId);
            var petEvent = this.eventsRepository.All().FirstOrDefault(x => x.Id == id);
            var shelter = this.sheltersRepository.All().FirstOrDefault(x => x.Id == id);

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

        // Add Subscription
        public async Task SubscribeAsync(string id, string email)
        {
            var subscription = new Subscription
            {
                UserId = id,
                Email = email,
            };

            await this.subscriptionsRepository.AddAsync(subscription);
            await this.subscriptionsRepository.SaveChangesAsync();
        }

        public bool IsSubscribed(string email)
        {
            return this.subscriptionsRepository
                .AllAsNoTracking()
                .Any(x => x.Email.Equals(email));
        }

        public Request GetRequestById(string id)
        {
            return this.requestsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public EmailViewModel GetPetEmailDetails(string id, string receiverId)
        {
            var receiver = this.usersRepository.All().FirstOrDefault(x => x.Id == receiverId);
            var pet = this.petsRepository.All().FirstOrDefault(x => x.Id == id);

            var html = new StringBuilder();
            html.AppendLine($"<h1>{pet.Name}</h1>");
            html.AppendLine($"<h3>{pet.Type}</h3>");
            html.AppendLine($"<h5>{pet.Description}</h3>");
            html.AppendLine($"<img src=\"https://s3.amazonaws.com/cdn-origin-etr.akc.org/wp-content/uploads/2018/03/13114640/golden-retriever-birthday-party-header.jpg\" />");

            var email = new EmailViewModel
            {
                SenderEmail = "ivanova.riva@gmail.com",
                SenderName = "PetGuide",
                ReceiverEmail = receiver.Email,
                Title = pet.Name,
                Content = html.ToString(),
            };

            return email;
        }

        public EmailViewModel GetVolunteerEmail(string receiverId)
        {
            var receiver = this.usersRepository.All().FirstOrDefault(x => x.Id == receiverId);

            var html = new StringBuilder();
            html.AppendLine($"<h1> PetGuide Volunteer </h1>");
            html.AppendLine($"<h3> We are happy that you decided to become a volunteer and help us. </h3>");
            html.AppendLine($"<h5> Volunteering is a voluntary act of an individual or group freely giving time and labour for community service. </h3>");
            html.AppendLine($"<img src=\"https://www.littlewhitedogco.com/wp-content/uploads/2019/10/volunteer-1200x630.jpg\" />");

            var email = new EmailViewModel
            {
                SenderEmail = "ivanova.riva@gmail.com",
                SenderName = "PetGuide",
                ReceiverEmail = receiver.Email,
                Title = "Volunteer",
                Content = html.ToString(),
            };

            return email;
        }
    }
}
