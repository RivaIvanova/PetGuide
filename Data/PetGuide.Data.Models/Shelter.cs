namespace PetGuide.Data.Models
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
        }

        [Required]
        [RegularExpression("^[A-Z][a-z]+ ([A-Z][a-z]+)*$")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Location { get; set; }

        public ICollection<Pet> Pets { get; set; }

        public ICollection<UserShelter> ShelterVolunteers { get; set; }

        // Shelter List
    }
}
