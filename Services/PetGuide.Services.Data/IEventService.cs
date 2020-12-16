namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Events;

    public interface IEventService
    {
        Task AddAsync(EventInputModel input, string userId);

        Task EditAsync(string id, EventInputModel input);

        Task DeleteAsync(string id);

        IEnumerable<AllEventsViewModel> GetAll(int compared);

        EventDetailsViewModel GetEventDetails(string id);

        EventInputModel GetEventEdit(string id);

        PetEvent GetEventById(string id);
    }
}
