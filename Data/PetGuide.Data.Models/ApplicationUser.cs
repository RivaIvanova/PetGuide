// ReSharper disable VirtualMemberCallInConstructor
namespace PetGuide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Identity;

    using PetGuide.Data.Common.Models;
    using PetGuide.Data.Models.Enums;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            // Custom collections
            this.Posts = new HashSet<Post>();
            this.Pets = new HashSet<Pet>();
            this.Shelters = new HashSet<UserShelter>();
            this.PetEvents = new HashSet<UserPetEvent>();
        }

        [Required]
        [RegularExpression("^[A-Z][a-z]+$")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[A-Z][a-z]+$")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Range(14, 104)]
        public int Age { get; set; }

        [Required]
        [MaxLength(150)]
        public string Address { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Pet> Pets { get; set; }

        public ICollection<UserShelter> Shelters { get; set; }

        public ICollection<UserPetEvent> PetEvents { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

        public ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
