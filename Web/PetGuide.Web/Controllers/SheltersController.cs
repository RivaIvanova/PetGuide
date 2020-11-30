namespace PetGuide.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Services.Data.Shelters;
    using PetGuide.Web.ViewModels.Shelters;

    public class SheltersController : Controller
    {
        private readonly IGetAllSheltersService getAllSheltersService;

        public SheltersController(
            IGetAllSheltersService getAllSheltersService)
        {
            this.getAllSheltersService = getAllSheltersService;
        }

        public IActionResult All()
        {
            var shelters = this.getAllSheltersService.GetAll();

            return this.View(shelters);
        }
    }
}
