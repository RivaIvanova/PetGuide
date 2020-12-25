namespace PetGuide.Web.ViewModels.Shelters
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using PetGuide.Data.Models;
    using PetGuide.Web.Infrastructure.Attributes;

    public abstract class BaseShelterViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Activities { get; set; }

        [Required]
        [PicturesMaxCount(10)]
        public ICollection<IFormFile> Pictures { get; set; }
    }
}
