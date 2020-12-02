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

            return this.Redirect("/Pets/All");
        }

        // Search Pets
        public IActionResult Search()
        {
            return this.View();
        }

        // Pets Details
        public IActionResult Details(string id)
        {
            var petsDetailsDto = this.petService.GetPetsDetails(id);

            var viewModel = new PetsDetailsViewModel
            {
                Name = petsDetailsDto.Name,
                CreatedOn = petsDetailsDto.CreatedOn,
                Age = petsDetailsDto.Age,
                Description = petsDetailsDto.Description,
                Contact = petsDetailsDto.Contact,
                Location = petsDetailsDto.Location,
            };

            return this.View(viewModel);
        }
    }
}
