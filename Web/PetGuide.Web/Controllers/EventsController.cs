namespace PetGuide.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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
    }
}
