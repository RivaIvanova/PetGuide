namespace PetGuide.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using PetGuide.Data.Common.Models;

    public class Request : BaseDeletableModel<string>
    {
        public Request()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey("ApplicationUser")]
        public string VolunteerId { get; set; }

        public ApplicationUser Volunteer { get; set; }

        [ForeignKey("PetEvent")]
        public string EventId { get; set; }

        public PetEvent Event { get; set; }

        [ForeignKey("Shelter")]
        public string ShelterId { get; set; }

        public Shelter Shelter { get; set; }
    }
}
