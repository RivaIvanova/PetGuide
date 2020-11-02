namespace PetGuide.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserShelter
    {
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey("Shelter")]
        public string ShelterId { get; set; }

        public Shelter Shelter { get; set; }
    }
}
