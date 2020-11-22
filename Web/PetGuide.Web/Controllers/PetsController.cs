namespace PetGuide.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using PetGuide.Services.Data.Pets;
    using PetGuide.Web.ViewModels.Pets;

    public class PetsController : BaseController
    {
        private readonly IGetPetsDetailsService getPetsDetailsService;

        public PetsController(IGetPetsDetailsService getPetsDetailsService)
        {
            this.getPetsDetailsService = getPetsDetailsService;
        }

        // Add Pets
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddPetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            // Redirect to All Pets
            return this.Redirect("/Pets/Search");
        }

        // Search Pets
        public IActionResult Search()
        {
            return this.View();
        }

        // Pets Details
        public IActionResult Details()
        {
            var petsDetailsDto = this.getPetsDetailsService.GetPetsDetails();

            var viewModel = new PetsDetailsViewModel
            {
                Name = petsDetailsDto.Name,
                Location = petsDetailsDto.Location,
                Age = petsDetailsDto.Age,
                Description = petsDetailsDto.Description,
                Contact = petsDetailsDto.Contact,
            };

            return this.View(viewModel);
        }
    }
}
