namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Web.ViewModels.Administration.Dashboard;

    public interface IRequestService
    {
        Task AddAsync(string id, string volunteerId);

        Task ApproveAsync(string id);

        Task DeclineAsync(string id);

        bool IsRequestSent(string id, string volunteerId);

        public IEnumerable<RequestViewModel> GetAll();

        public IndexViewModel GetDashboard();
    }
}
