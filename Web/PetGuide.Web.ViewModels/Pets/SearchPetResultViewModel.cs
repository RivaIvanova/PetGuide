namespace PetGuide.Web.ViewModels.Pets
{
    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;
    using PetGuide.Services.Mapping;

    public class SearchPetResultViewModel : IMapFrom<Pet>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public PetType Type { get; set; }

        public PetStatus Status { get; set; }
    }
}
