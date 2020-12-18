﻿namespace PetGuide.Web.Controllers
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
        private readonly IRequestService requestService;

        public EventsController(
            IEventService eventService,
            IRequestService requestService)
        {
            this.eventService = eventService;
            this.requestService = requestService;
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

            await this.eventService.AddAsync(input);
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
        public async Task<IActionResult> Edit(string id, EventInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
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

        // Add Volunteer Request
        public IActionResult VolunteerSuccess(string id)
        {
            var viewModel = new VolunteerSuccessViewModel { Id = id };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Volunteer(string id)
        {
            var volunteerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (this.requestService.IsRequestSent(id, volunteerId))
            {
                return this.BadRequest();
            }

            await this.requestService.AddAsync(id, volunteerId);
            return this.RedirectToAction(nameof(this.VolunteerSuccess), new { id });
        }
    }
}
