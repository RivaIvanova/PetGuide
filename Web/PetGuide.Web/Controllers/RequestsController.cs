namespace PetGuide.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Services.Data;
    using PetGuide.Services.Messaging;

    public class RequestsController : Controller
    {
        private readonly IRequestService requestService;
        private readonly IEmailSender emailSender;

        public RequestsController(
            IRequestService requestService,
            IEmailSender emailSender)
        {
            this.requestService = requestService;
            this.emailSender = emailSender;
        }

        // Send Pet Details to Email
        [HttpPost]
        public async Task<IActionResult> SendToEmail(string id)
        {
            var receiverId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var email = this.requestService.GetPetEmailDetails(id, receiverId);

            await this.emailSender.SendEmailAsync(email.SenderEmail, email.SenderName, email.ReceiverEmail, email.Title, email.Content);
            return this.RedirectToAction("All", "Pets");
        }

        // Add Volunteer Request
        [HttpPost]
        public async Task<IActionResult> Volunteer(string id)
        {
            var volunteerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (this.requestService.IsRequestSent(id, volunteerId))
            {
                return this.BadRequest();
            }

            await this.requestService.AddAsync(id, volunteerId);
            return this.RedirectToAction(nameof(this.VolunteerSuccess));
        }

        // Volunteer Request Success
        public IActionResult VolunteerSuccess()
        {
            return this.View();
        }

        // Subscribe Request
        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (this.requestService.IsSubscribed(email))
            {
                return this.BadRequest();
            }

            string subscriberId = string.Empty;

            if (this.User.Identity.IsAuthenticated)
            {
                subscriberId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                subscriberId = null;
            }

            await this.requestService.SubscribeAsync(subscriberId, email);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
