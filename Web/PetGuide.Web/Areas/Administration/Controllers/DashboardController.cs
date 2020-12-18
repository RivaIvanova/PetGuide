namespace PetGuide.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Services.Data;

    public class DashboardController : AdministrationController
    {
        private readonly IRequestService requestService;

        public DashboardController(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        [Authorize(Policy = "DashboardRoles")]
        public IActionResult Index()
        {
            var viewModel = this.requestService.GetDashboard();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DashboardRoles")]
        public async Task<IActionResult> Approve(string id)
        {
            await this.requestService.ApproveAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [Authorize(Policy = "DashboardRoles")]
        public async Task<IActionResult> Decline(string id)
        {
            await this.requestService.DeclineAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
