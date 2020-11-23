namespace PetGuide.Services.Data.DTOs
{
    using PetGuide.Data.Models;

    public class PetsDetailsDto
    {
        public string Name { get; set; }

        public Location Location { get; set; }

        public int? Age { get; set; }

        public string Description { get; set; }

        public ApplicationUser Contact { get; set; }
    }
}
