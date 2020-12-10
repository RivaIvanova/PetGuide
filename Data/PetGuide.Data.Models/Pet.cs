namespace PetGuide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;
    using PetGuide.Data.Models.Enums;

    public class Pet : BaseDeletableModel<string>
    {
        public Pet()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Pictures = new HashSet<Picture>();
        }

        [Required]
        [RegularExpression(@"^[A-Z][a-z]+\s?(([A-Z][a-z]+)\s?){0,3}$")]
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

        public int LocationId { get; set; }

        [Required]
        public Location Location { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ShelterId { get; set; }

        public Shelter Shelter { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
