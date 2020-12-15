namespace PetGuide.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Events;

    public class EventService : IEventService
    {
        private readonly IDeletableEntityRepository<PetEvent> eventsRepository;
        private readonly ILocationService locationService;

        public EventService(
            IDeletableEntityRepository<PetEvent> eventsRepository,
            ILocationService locationService)
        {
            this.eventsRepository = eventsRepository;
            this.locationService = locationService;
        }

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

        public async Task DeleteAsync(string id)
        {
            var petEvent = this.GetEventById(id);
            this.eventsRepository.Delete(petEvent);
            await this.eventsRepository.SaveChangesAsync();
        }

        public Task EditAsync(string id, EventInputModel input)
        {
            throw new NotImplementedException();
        }

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
