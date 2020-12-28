namespace PetGuide.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using PetGuide.Services.Data;
    using PetGuide.Web.ViewModels.Pictures;

    public class PicturesController : Controller
    {
        private readonly IPictureService picturesService;

        public PicturesController(IPictureService picturesService)
        {
            this.picturesService = picturesService;
        }

        [HttpGet]
        public IActionResult Upload(string id)
        {
            var viewModel = this.picturesService.GetUpload(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> Upload(UploadPicturesInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.picturesService.Upload(
                input.Pictures.Select(i => new PictureInputModel
                {
                    Name = i.FileName,
                    Type = i.ContentType,
                    Content = i.OpenReadStream(),
                }),
                userId,
                input.Id);

            return this.RedirectToAction(nameof(this.Gallery));
        }

        public IActionResult Gallery()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = this.picturesService.GetGallery(userId);

            return this.View(viewModel);
        }
    }
}
