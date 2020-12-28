namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Web.ViewModels.Pictures;

    public interface IPictureService
    {
        Task Upload(IEnumerable<PictureInputModel> pictures, string userId, string id);

        Task DeleteAsync(string id);

        Gallery GetGallery(string userId);

        UploadPicturesInputModel GetUpload(string id);

        IEnumerable<PictureViewModel> GetPicturesToShow(string id);

        // Delete
        IEnumerable<PictureViewModel> GetEventsPictures(string id);

        IEnumerable<PictureViewModel> GetSheltersPictures(string id);

        IEnumerable<PictureViewModel> GetPetsPictures(string id);
    }
}
