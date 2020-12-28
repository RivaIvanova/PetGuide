namespace PetGuide.Web.ViewModels.Shelters
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using PetGuide.Data.Models.Enums;
    using PetGuide.Web.Infrastructure.Attributes;

    public class AddPetToShelterInputModel
    {
        [Required]
        [RegularExpression(@"^[A-Z][a-z]+\s?(([A-Z][a-z]+)\s?){0,3}$", ErrorMessage = "Name should start with capital letter and could be four parts.")]
        [MaxLength(30)]
        public string Name { get; set; }

        public int? Age { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public PetType Type { get; set; }

        [Required]
        public PetStatus Status { get; set; }

        [Required]
        public PetSize Size { get; set; }

        [Required]
        public PetColor Color { get; set; }

        [Required]
        [PicturesMaxCount(5)]
        public ICollection<IFormFile> Pictures { get; set; }
    }
}
