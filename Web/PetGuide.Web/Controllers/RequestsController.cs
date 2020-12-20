namespace PetGuide.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Services.Data;
    using PetGuide.Web.ViewModels.Events;

    public class RequestsController : Controller
    {
        //private readonly IRequestService requestService;

        //public RequestsController(IRequestService requestService)
        //{
        //    this.requestService = requestService;
        //}

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
