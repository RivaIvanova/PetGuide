namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Events;

    public interface IEventService
    {
        Task AddAsync(EventInputModel input, string userId);

        Task EditAsync(string id, EditEventInputModel input);

        Task DeleteAsync(string id);

        IEnumerable<AllEventsViewModel> GetAll(int compared);

        IEnumerable<AllEventsViewModel> GetVolunteerEvents(string id);

        EventDetailsViewModel GetEventDetails(string id);

        EditEventInputModel GetEventEdit(string id);

        PetEvent GetEventById(string id);
    }
}
