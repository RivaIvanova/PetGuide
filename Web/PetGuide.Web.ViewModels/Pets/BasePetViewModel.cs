namespace PetGuide.Web.ViewModels.Pets
{
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;

    public abstract class BasePetViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Z][a-z]+\s?(([A-Z][a-z]+)\s?){0,3}$", ErrorMessage = "Name should start with capital letter and could be four parts.")]
        [MaxLength(30)]
        public string Name { get; set; }

        public int? Age { get; set; }

        [Required]
        public Location Location { get; set;  }

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
    }
}
