namespace PetGuide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;
    using PetGuide.Web.Infrastructure.Attributes;

    public class PetEvent : BaseDeletableModel<string>
    {
        public PetEvent()
        {
            this.Id = Guid.NewGuid().ToString();
            this.EventVolunteers = new HashSet<UserPetEvent>();
            this.Pictures = new HashSet<Picture>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Purpose { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Activities { get; set; }

        [Required]
        [DateMinValue(0)]
        public DateTime DateTime { get; set; }

        public int LocationId { get; set; }

        [Required]
        public Location Location { get; set; }

        public ICollection<UserPetEvent> EventVolunteers { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
