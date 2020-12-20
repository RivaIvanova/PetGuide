namespace PetGuide.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Services.Data;
    using PetGuide.Services.Messaging;
    using PetGuide.Web.ViewModels.Events;

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

        [HttpPost]
        public async Task<IActionResult> SendToEmail(string id)
        {
            var receiverId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var email = this.requestService.GetEventEmailDetails(id, receiverId);

            await this.emailSender.SendEmailAsync(email.SenderEmail, email.SenderName, "tamolar835@boersy.com", email.Title, email.Content);
            return this.RedirectToAction("All", "Events");
        }

        //// Add Volunteer Request

        ////[HttpPost]
        ////public async Task<IActionResult> Volunteer(string id)
        ////{
        ////    var volunteerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        ////    if (this.requestService.IsRequestSent(id, volunteerId))
        ////    {
        ////        return this.BadRequest();
        ////    }

        ////    await this.requestService.AddAsync(id, volunteerId);
        ////    return this.RedirectToAction(nameof(this.VolunteerSuccess), new { id });
        ////}
    }
}
