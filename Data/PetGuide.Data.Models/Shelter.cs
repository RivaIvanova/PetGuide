﻿namespace PetGuide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;

    public class Shelter : BaseModel<string>
    {
        public Shelter()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ShelterVolunteers = new HashSet<UserShelter>();
            this.Pictures = new HashSet<Picture>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int LocationId { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Activities { get; set; }

        public ICollection<Pet> Pets { get; set; }

        public ICollection<UserShelter> ShelterVolunteers { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
