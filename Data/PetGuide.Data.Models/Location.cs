namespace PetGuide.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;

    public class Location : BaseDeletableModel<int>
    {
        public District District { get; set; }

        [Required]
        [MaxLength(50)]
        public string Street { get; set; }

        [MaxLength(150)]
        public string AdditionalInfo { get; set; }
    }
}
