namespace PetGuide.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Services.Data.Pets;
    using PetGuide.Web.ViewModels.Pets;

    public class PetsController : BaseController
    {
        private readonly IAddPetService addPetService;
        private readonly IGetPetsDetailsService getPetsDetailsService;
        private readonly IGetAllPetsService getAllPetsService;

        public PetsController(
            IAddPetService addPetService,
            IGetPetsDetailsService getPetsDetailsService,
            IGetAllPetsService getAllPetsService)
        {
            this.addPetService = addPetService;
            this.getPetsDetailsService = getPetsDetailsService;
            this.getAllPetsService = getAllPetsService;
        }

        public IActionResult All()
        {
            var pets = this.getAllPetsService.GetAll();

            return this.View(pets);
        }

        // Add Pets
        public IActionResult Add()
        {
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddPetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.addPetService.AddAsync(input);

            // Redirect to All Pets
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
            var petsDetailsDto = this.getPetsDetailsService.GetPetsDetails(id);

            var viewModel = new PetsDetailsViewModel
            {
                Name = petsDetailsDto.Name,
                CreatedOn = petsDetailsDto.CreatedOn,
                Age = petsDetailsDto.Age,
                Description = petsDetailsDto.Description,
                Contact = petsDetailsDto.Contact,
            };

            return this.View(viewModel);
        }
    }
}
