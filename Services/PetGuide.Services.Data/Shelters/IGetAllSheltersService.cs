namespace PetGuide.Services.Data.Shelters
{
    using System.Collections.Generic;

    using PetGuide.Web.ViewModels.Shelters;

    public interface IGetAllSheltersService
    {
        ICollection<AllSheltersViewModel> GetAll();
    }
}
