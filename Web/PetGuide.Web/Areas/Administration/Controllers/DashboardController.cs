namespace PetGuide.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Common;
    using PetGuide.Data.Models;
    using PetGuide.Services.Data;
    using PetGuide.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : BaseController
    {
        private readonly IDashboardService dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [Authorize(Policy = "DashboardRoles")]
        public async Task<IActionResult> Index()
        {
            var viewModel = this.dashboardService.GetDashboard();
            return this.View(viewModel);
        }

    }
}
