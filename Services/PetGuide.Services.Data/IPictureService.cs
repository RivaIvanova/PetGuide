namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Web.ViewModels.Pictures;

    public interface IPictureService
    {
        Task Upload(IEnumerable<PictureInputModel> pictures, string userId = null, string petId = null, string eventId = null, string shelterId = null, string postId = null);

        Task DeleteAsync(string id);

        Task EditAsync(IEnumerable<PictureViewModel> pictures);

        Task<List<string>> GetAllPictures();

        IEnumerable<PictureViewModel> GetEventsPictures(string id);

        IEnumerable<PictureViewModel> GetSheltersPictures(string id);

        IEnumerable<PictureViewModel> GetPetsPictures(string id);
    }
}
