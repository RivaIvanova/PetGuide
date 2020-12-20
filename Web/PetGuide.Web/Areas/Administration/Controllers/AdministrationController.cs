namespace PetGuide.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Common;
    using PetGuide.Services.Data;
    using PetGuide.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IRequestService requestsService;
        private readonly IShelterService sheltersService;
        private readonly IEventService eventsService;

        public AdministrationController(
            IRequestService requestsService,
            IShelterService sheltersService,
            IEventService eventsService)
        {
            this.requestsService = requestsService;
            this.sheltersService = sheltersService;
            this.eventsService = eventsService;
        }

        [HttpPost]
        [Authorize(Policy = "DashboardRoles")]
        public async Task<IActionResult> Approve(string id)
        {
            await this.requestsService.ApproveAsync(id);
            return this.RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [Authorize(Policy = "DashboardRoles")]
        public async Task<IActionResult> Decline(string id)
        {
            await this.requestsService.DeclineAsync(id);
            return this.RedirectToAction("Index", "Dashboard");
        }

        [Authorize(Policy = "DashboardRoles")]
        public IActionResult Shelters()
        {
            var viewModel = this.sheltersService.SheltersAdminView();

            return this.View(viewModel);
        }

        [Authorize(Policy = "DashboardRoles")]
        public IActionResult Events()
        {
            var viewModel = this.eventsService.EventsAdminView();

            return this.View(viewModel);
        }
    }
}
