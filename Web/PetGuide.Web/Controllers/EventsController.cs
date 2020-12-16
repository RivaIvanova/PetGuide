namespace PetGuide.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Common;
    using PetGuide.Services.Data;
    using PetGuide.Web.ViewModels.Events;

    public class EventsController : BaseController
    {
        private readonly IEventService eventService;

        public EventsController(
            IEventService eventService)
        {
            this.eventService = eventService;
        }

        // Add Event
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(EventInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.eventService.AddAsync(input, userId);

            return this.Redirect("/");
        }

        // Get All Events
        public IActionResult All()
        {
            var todaysEvents = this.eventService.GetAll(0);
            var upcommingEvents = this.eventService.GetAll(1);
            var pastEvents = this.eventService.GetAll(-1);

            var viewModel = new AllEventsListViewModel
            {
                TodaysEvents = todaysEvents,
                UpcommingEvents = upcommingEvents,
                PastEvents = pastEvents,
            };

            return this.View(viewModel);
        }

        // Get Event Details View
        public IActionResult Details(string id)
        {
            var viewModel = this.eventService.GetEventDetails(id);

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(string id)
        {
            var inputModel = this.eventService.GetEventEdit(id);
            return this.View(inputModel);
        }
    }
}
