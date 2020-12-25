namespace PetGuide.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using PetGuide.Services.Data;
    using PetGuide.Web.ViewModels.Pictures;

    public class PicturesController : Controller
    {
        private readonly IPictureService pictureService;

        public PicturesController(IPictureService pictureService)
        {
            this.pictureService = pictureService;
        }

        [HttpGet]
        public IActionResult Upload() => this.View();

        [HttpPost]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> Upload(IFormFile[] pictures)
        {
            if (pictures.Length > 5)
            {
                this.ModelState.AddModelError("pictures", "You cannot upload more than 5 images!");
                return this.View();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.pictureService.Upload(
                pictures.Select(i => new PictureInputModel
            {
                Name = i.FileName,
                Type = i.ContentType,
                Content = i.OpenReadStream(),
            }), userId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            return this.View(await this.pictureService.GetAllPictures());
        }
    }
}
