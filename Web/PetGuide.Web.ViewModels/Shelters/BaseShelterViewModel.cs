namespace PetGuide.Web.ViewModels.Shelters
{
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Models;

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
    }
}
