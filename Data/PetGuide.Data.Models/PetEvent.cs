namespace PetGuide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;

    public class PetEvent : BaseDeletableModel<string>
    {
        public PetEvent()
        {
            this.Id = Guid.NewGuid().ToString();
            this.EventVolunteers = new HashSet<UserPetEvent>();
            this.Pictures = new HashSet<Picture>();
        }

        [Required]
        [RegularExpression("^[A-Z][a-z]+ ([A-Z][a-z]+)*$")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int LocationId { get; set; }

        [Required]
        public Location Location { get; set; }

        public ICollection<UserPetEvent> EventVolunteers { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
