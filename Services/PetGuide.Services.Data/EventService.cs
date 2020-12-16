namespace PetGuide.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Events;

    public class EventService : IEventService
    {
        private readonly IDeletableEntityRepository<PetEvent> eventsRepository;
        private readonly IDeletableEntityRepository<Location> locationsRepository;
        private readonly ILocationService locationService;

        public EventService(
            IDeletableEntityRepository<PetEvent> eventsRepository,
            IDeletableEntityRepository<Location> locationsRepository,
            ILocationService locationService)
        {
            this.eventsRepository = eventsRepository;
            this.locationsRepository = locationsRepository;
            this.locationService = locationService;
        }

        // Add Event
        public async Task AddAsync(EventInputModel input, string userId)
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
        }

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

        // Edit Event
        public Task EditAsync(string id, EventInputModel input)
        {
            throw new NotImplementedException();
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
                   Date = x.DateTime.Date.ToString("d"),
                   Time = x.DateTime.TimeOfDay.ToString("t"),
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
                Date = petEvent.DateTime.Date.ToString("d"),
                Time = petEvent.DateTime.TimeOfDay.ToString("t"),
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

        private DateTime GetDateAndTime(DateTime date, DateTime time)
        {
            var dt = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

            return dt;
        }
    }
}
