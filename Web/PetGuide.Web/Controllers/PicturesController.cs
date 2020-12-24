namespace PetGuide.Web.Controllers
{
    using System.Linq;
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
        [RequestSizeLimit(100 * 1024 * 1024)]
        public async Task<IActionResult> Upload(IFormFile[] pictures)
        {
            if (pictures.Length > 10)
            {
                this.ModelState.AddModelError("images", "You cannot upload more than 10 images!");
                return this.View();
            }

            await this.pictureService.Upload(pictures.Select(i => new PictureInputModel
            {
                Name = i.FileName,
                Type = i.ContentType,
                Content = i.OpenReadStream(),
            }));

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            return this.View(await this.pictureService.GetAllPictures());
        }
    }
}
