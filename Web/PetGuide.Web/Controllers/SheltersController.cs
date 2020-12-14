namespace PetGuide.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Common;
    using PetGuide.Services.Data;
    using PetGuide.Web.ViewModels.Pets;
    using PetGuide.Web.ViewModels.Shelters;

    public class SheltersController : Controller
    {
        private readonly IShelterService sheltersService;
        private readonly IPetService petService;

        public SheltersController(
            IShelterService sheltersService,
            IPetService petService)
        {
            this.sheltersService = sheltersService;
            this.petService = petService;
        }

        public IActionResult All()
        {
            var shelters = this.sheltersService.GetAll();

            return this.View(shelters);
        }

        // Add Shelter
        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ShelterInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.sheltersService.AddAsync(input);

            return this.RedirectToAction(nameof(this.All));
        }

        // Edit Shelter
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(string id)
        {
            var inputModel = this.sheltersService.GetShelterEdit(id);
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(string id, ShelterInputModel input)
        {
            var inputModel = this.sheltersService.GetShelterEdit(id);

            var shelter = this.sheltersService.GetShelterById(id);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.sheltersService.EditAsync(id, input);
            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        // Shelter Details
        public IActionResult Details(string id)
        {
            var viewModel = this.sheltersService.GetSheltesDetails(id);

            return this.View(viewModel);
        }

        // Add Pet To Shelter
        [Authorize]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Pet()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Administrator,ShelterModerator")]
        public async Task<IActionResult> Pet(string id, AddPetToShelterInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.sheltersService.AddAsync(id, input, userId);

            return this.RedirectToAction(nameof(this.Details), new { id });
        }
    }
}
