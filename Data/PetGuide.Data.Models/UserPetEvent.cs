namespace PetGuide.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserPetEvent
    {
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey("PetEvent")]
        public string PetEventId { get; set; }

        public PetEvent PetEvent { get; set; }
    }
}
