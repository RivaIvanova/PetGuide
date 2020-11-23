namespace PetGuide.Services.Data.Pets
{
    using System.Threading.Tasks;

    using PetGuide.Web.ViewModels.Pets;

    public interface IAddPetService
    {
        Task AddAsync(AddPetInputModel input);
    }
}
