namespace PetGuide.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using PetGuide.Web.Infrastructure.Attributes;
    using PetGuide.Web.ViewModels.Pictures;

    public class EditEventInputModel : BaseEventInputModel
    {
        [Required]
        [DateMinValue(0)]
        public DateTime Date { get; set; }

        [PicturesMaxCount(5)]
        public ICollection<IFormFile> Pictures { get; set; }

        public IEnumerable<PictureViewModel> PicturesToShow { get; set; }
    }
}
