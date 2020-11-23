namespace PetGuide.Services.Data.Pets
{
    using PetGuide.Services.Data.DTOs;

    public interface IGetPetsDetailsService
    {
        PetsDetailsDto GetPetsDetails();
    }
}
