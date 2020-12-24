namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Web.ViewModels.Pictures;

    public interface IPictureService
    {
        Task Upload(IEnumerable<PictureInputModel> pictures, string userId = null, string petId = null, string eventId = null, string shelterId = null);

        Task<List<string>> GetAllPictures();
    }
}
