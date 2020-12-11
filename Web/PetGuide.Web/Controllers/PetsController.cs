namespace PetGuide.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Services.Data;

    using PetGuide.Web.ViewModels.Pets;

    public class PetsController : BaseController
    {
        private readonly IPetService petService;

        public PetsController(
            IPetService petService)
        {
            this.petService = petService;
        }

        // All Pets
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                id = 1;
            }

            const int itemsPerPage = 12;

            var viewModel = new AllPetsListViewModel
            {
                PageNumber = id,
                ItemsCount = this.petService.GetCount(),
                ItemsPerPage = itemsPerPage,
                Pets = this.petService.GetAll(id, itemsPerPage),
            };

            return this.View(viewModel);
        }

        // Add Pets
        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddPetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.petService.AddAsync(input, userId);

            return this.RedirectToAction(nameof(this.All));
        }

        // Edit Pets
        [Authorize]
        public IActionResult Edit(string id)
        {
            var pet = this.petService.GetPetById(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (pet.UserId != userId)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var inputModel = this.petService.GetPetEdit(id);
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, EditPetInputModel input)
        {
            var pet = this.petService.GetPetById(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (pet.UserId != userId)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.petService.EditAsync(id, input);
            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        // Delete Pets
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var pet = this.petService.GetPetById(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (pet.UserId == userId)
            {
                await this.petService.DeleteAsync(id);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        // Search Pets
        public IActionResult Search()
        {
            var viewModel = new SearchPetListViewModel
            {
                Age = default,
                Color = default,
                Size = default,
                Status = default,
                Type = default,
                District = default,
                Street = default,
                Description = default,
                Pets = this.petService.GetRecentlyAdded(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(SearchPetListViewModel input)
        {
            var viewModel = this.petService.SetSearchValues(input);
            viewModel.Pets = this.petService.SearchPets(input);

            return this.View(viewModel);
        }

        // Pets Details
        public IActionResult Details(string id)
        {
            var viewModel = this.petService.GetPetsDetails(id);

            return this.View(viewModel);
        }
    }
}
