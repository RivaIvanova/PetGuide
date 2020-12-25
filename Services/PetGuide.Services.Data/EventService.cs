namespace PetGuide.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Administration.Events;
    using PetGuide.Web.ViewModels.Events;
    using PetGuide.Web.ViewModels.Pictures;

    public class EventService : IEventService
    {
        private readonly IDeletableEntityRepository<PetEvent> eventsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Location> locationsRepository;
        private readonly ILocationService locationService;
        private readonly IPictureService picturesService;

        public EventService(
            IDeletableEntityRepository<PetEvent> eventsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Location> locationsRepository,
            ILocationService locationService, 
            IPictureService picturesService)
        {
            this.eventsRepository = eventsRepository;
            this.usersRepository = usersRepository;
            this.locationsRepository = locationsRepository;
            this.locationService = locationService;
            this.picturesService = picturesService;
        }

        // Add Event
        public async Task AddAsync(EventInputModel input)
        {
            var location = this.locationService.GetLocation(input.Location.District, input.Location.Street, input.Location.AdditionalLocationInfo);
            var dateAndTime = this.GetDateAndTime(input.Date, input.Time);

            var petEvent = new PetEvent
            {
                Name = input.Name,
                Purpose = input.Purpose,
                Description = input.Description,
                Activities = input.Activities,
                DateTime = dateAndTime,
                Location = location,
            };

            await this.eventsRepository.AddAsync(petEvent);
            await this.eventsRepository.SaveChangesAsync();

            await this.picturesService.Upload(
              input.Pictures.Select(i => new PictureInputModel
              {
                  Name = i.FileName,
                  Type = i.ContentType,
                  Content = i.OpenReadStream(),
              }),
              null, null, petEvent.Id);
        }

        // Edit Event
        public EventInputModel GetEventEdit(string id)
        {
            var petEvent = this.eventsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            var location = this.locationsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == petEvent.LocationId);

            var viewModel = new EventInputModel
            {
                Name = petEvent.Name,
                Purpose = petEvent.Purpose,
                Date = petEvent.DateTime.Date,
                Time = petEvent.DateTime,
                Description = petEvent.Description,
                Activities = petEvent.Activities,
                Location = location,
            };

            return viewModel;
        }

        public async Task EditAsync(string id, EventInputModel input)
        {
            var petEvent = this.GetEventById(id);
            var location = this.locationService.GetLocation(input.Location.District, input.Location.Street, input.Location.AdditionalLocationInfo);
            var dateAndTime = this.GetDateAndTime(input.Date, input.Time);

            petEvent.Name = input.Name;
            petEvent.Purpose = input.Purpose;
            petEvent.Description = input.Description;
            petEvent.Activities = input.Activities;
            petEvent.DateTime = dateAndTime;
            petEvent.Location = location;

            await this.eventsRepository.SaveChangesAsync();
        }

        // Delete Event
        public async Task DeleteAsync(string id)
        {
            var petEvent = this.GetEventById(id);
            this.eventsRepository.Delete(petEvent);
            await this.eventsRepository.SaveChangesAsync();
        }

        // Get All Events
        public IEnumerable<AllEventsViewModel> GetAll(int compared)
        {
            return this.eventsRepository
               .AllAsNoTracking()
               .Where(x => x.DateTime.Date.CompareTo(DateTime.UtcNow.Date) == compared)
               .Select(x => new AllEventsViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Purpose = x.Purpose,
                   Date = x.DateTime,
                   Time = x.DateTime,
                   Description = x.Description,
                   Location = x.Location,
               })
               .ToList();
        }

        // Get Volunteer Events
        public IEnumerable<AllEventsViewModel> GetVolunteerEvents(string id)
        {
            return this.eventsRepository
               .AllAsNoTracking()
               .Where(x => x.EventVolunteers.Any(x => x.UserId == id))
               .Select(x => new AllEventsViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Purpose = x.Purpose,
                   Date = x.DateTime,
                   Time = x.DateTime,
                   Description = x.Description,
                   Location = x.Location,
               })
               .ToList();
        }

        // Get Event Details
        public EventDetailsViewModel GetEventDetails(string id)
        {
            var petEvent = this.eventsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            var location = this.locationsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == petEvent.LocationId);

            var viewModel = new EventDetailsViewModel
            {
                Id = petEvent.Id,
                Name = petEvent.Name,
                Purpose = petEvent.Purpose,
                Date = petEvent.DateTime,
                Time = petEvent.DateTime,
                Description = petEvent.Description,
                Activities = petEvent.Activities,
                Location = location,
            };

            return viewModel;
        }

        // Get Event By Id
        public PetEvent GetEventById(string id)
        {
            return this.eventsRepository.All().FirstOrDefault(x => x.Id == id);
        }

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

        private DateTime GetDateAndTime(DateTime date, DateTime time)
        {
            var dt = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

            return dt;
        }
    }
}
