namespace PetGuide.Services.Data
{
    using PetGuide.Web.ViewModels.Administration.Dashboard;

    public interface IDashboardService
    {
        DashboardViewModel GetDashboard();
    }
}
