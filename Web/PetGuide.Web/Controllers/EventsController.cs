namespace PetGuide.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Common;
    using PetGuide.Data.Models;
    using PetGuide.Services.Data;
    using PetGuide.Web.ViewModels.Events;

    public class EventsController : BaseController
    {
        private readonly IEventService eventService;
        private readonly IRequestService requestService;
        private readonly UserManager<ApplicationUser> userManager;

        public EventsController(
            IEventService eventService,
            IRequestService requestService,
            UserManager<ApplicationUser> userManager)
        {
            this.eventService = eventService;
            this.requestService = requestService;
            this.userManager = userManager;
        }

        // Get All Events
        public IActionResult All()
        {
            var userId = this.userManager.GetUserId(this.User);

            var todaysEvents = this.eventService.GetAll(0);
            var upcommingEvents = this.eventService.GetAll(1);
            var pastEvents = this.eventService.GetAll(-1);
            var volunteerEvents = this.eventService.GetVolunteerEvents(userId);

            var viewModel = new AllEventsListViewModel
            {
                TodaysEvents = todaysEvents,
                UpcommingEvents = upcommingEvents,
                PastEvents = pastEvents,
                VolunteerEvents = volunteerEvents,
            };

            return this.View(viewModel);
        }

        // Get Event Details View
        public IActionResult Details(string id)
        {
            var viewModel = this.eventService.GetEventDetails(id);

            return this.View(viewModel);
        }

        // Add Event
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Add(EventInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.eventService.AddAsync(input, userId);
            return this.RedirectToAction(nameof(this.All));
        }

        // Edit Event
        [Authorize(Policy = "EventRoles")]
        public IActionResult Edit(string id)
        {
            var inputModel = this.eventService.GetEventEdit(id);
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Policy = "EventRoles")]
        public async Task<IActionResult> Edit(string id, EditEventInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var inputModel = this.eventService.GetEventEdit(id);
                return this.View(inputModel);
            }

            await this.eventService.EditAsync(id, input);
            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        // Delete Event
        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.eventService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
