namespace PetGuide.Services.Data
{
    using System.Collections.Generic;

    using PetGuide.Web.ViewModels.Administration.Dashboard;
    using PetGuide.Web.ViewModels.Administration.Events;
    using PetGuide.Web.ViewModels.Administration.Pets;
    using PetGuide.Web.ViewModels.Administration.Shelters;

    public interface IDashboardService
    {
        DashboardViewModel GetDashboard();

        IEnumerable<EventViewModel> GetAllEventsAdminView();

        EventsListViewModel EventsAdminView();

        IEnumerable<ShelterViewModel> GetAllSheltersAdminView();

        SheltersListViewModel SheltersAdminView();

        IEnumerable<PetsViewModel> GetAllPetsAdminView();

        PetsListViewModel PetsAdminView();
    }
}
