namespace PetGuide.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;
    using PetGuide.Data.Models.Enums;

    public class Location : BaseDeletableModel<int>
    {
        [Required]
        public District District { get; set; }

        [Required]
        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(250)]
        public string AdditionalLocationInfo { get; set; }
    }
}
