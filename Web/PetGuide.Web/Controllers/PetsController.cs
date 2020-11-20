namespace PetGuide.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PetsController : BaseController
    {
        public IActionResult Search()
        {
            return this.View();
        }
    }
}
