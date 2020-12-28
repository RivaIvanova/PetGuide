namespace PetGuide.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Common;
    using PetGuide.Services.Data;
    using PetGuide.Services.Messaging;
    using PetGuide.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IRequestService requestsService;
        private readonly IEmailSender emailSender;
        private readonly IDashboardService dashboardService;

        public AdministrationController(
            IRequestService requestsService,
            IEmailSender emailSender,
            IDashboardService dashboardService)
        {
            this.requestsService = requestsService;
            this.emailSender = emailSender;
            this.dashboardService = dashboardService;
        }

        [HttpPost]
        [Authorize(Policy = "DashboardRoles")]
        public async Task<IActionResult> Approve(string id)
        {
            await this.requestsService.ApproveAsync(id);

            var receiverId = this.requestsService.GetRequestById(id).VolunteerId;
            var email = this.requestsService.GetVolunteerEmail(receiverId);

            await this.emailSender.SendEmailAsync(email.SenderEmail, email.SenderName, email.ReceiverEmail, email.Title, email.Content);

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
            var viewModel = this.dashboardService.SheltersAdminView();

            return this.View(viewModel);
        }

        [Authorize(Policy = "DashboardRoles")]
        public IActionResult Events()
        {
            var viewModel = this.dashboardService.EventsAdminView();

            return this.View(viewModel);
        }

        [Authorize(Policy = "DashboardRoles")]
        public IActionResult Pets()
        {
            var viewModel = this.dashboardService.PetsAdminView();

            return this.View(viewModel);
        }
    }
}
