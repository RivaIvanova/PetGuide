namespace PetGuide.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;

    public class Pet : BaseDeletableModel<string>
    {
        public Pet()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [RegularExpression("^[A-Z][a-z]+ ([A-Z][a-z]+)*$")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Location { get; set; }

        public int? Age { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        // Type of Pet (Lost, Found, Spotted) - Enum or Roles
        [Required]
        public PetType Type { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ShelterId { get; set; }

        public Shelter Shelter { get; set; }
    }
}
