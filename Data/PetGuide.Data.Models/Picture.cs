namespace PetGuide.Data.Models
{
    using System;

    using PetGuide.Data.Common.Models;

    public class Picture : BaseDeletableModel<string>
    {
        public Picture()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // Content will be saved on file system
        public string Extension { get; set; }

        public string PetId { get; set; }

        public Pet Pet { get; set; }

        public string PetEventId { get; set; }

        public PetEvent PetEvent { get; set; }

        public string ShelterId { get; set; }

        public Shelter Shelter { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
