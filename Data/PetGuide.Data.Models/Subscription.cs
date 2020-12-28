namespace PetGuide.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;

    public class Subscription : BaseDeletableModel<string>
    {
        public Subscription()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
