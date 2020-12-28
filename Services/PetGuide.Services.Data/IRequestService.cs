namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Administration.Dashboard;
    using PetGuide.Web.ViewModels.Requests;

    public interface IRequestService
    {
        Task AddAsync(string id, string volunteerId);

        Task ApproveAsync(string id);

        Task DeclineAsync(string id);

        Task SubscribeAsync(string id, string email);

        bool IsSubscribed(string email);

        bool IsRequestSent(string id, string volunteerId);

        IEnumerable<RequestViewModel> GetAll();

        Request GetRequestById(string id);

        EmailViewModel GetPetEmailDetails(string id, string receiverId);

        EmailViewModel GetVolunteerEmail(string receiverId);
    }
}
