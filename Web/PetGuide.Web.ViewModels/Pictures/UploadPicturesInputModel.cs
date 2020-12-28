namespace PetGuide.Web.ViewModels.Pictures
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using PetGuide.Web.Infrastructure.Attributes;

    public class UploadPicturesInputModel : Gallery
    {
        public string Id { get; set; }

        [Required]
        [PicturesMaxCount(5)]
        public ICollection<IFormFile> Pictures { get; set; }
    }
}
