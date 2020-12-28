namespace PetGuide.Web.ViewModels.Pets
{
    using System.Collections.Generic;

    public class MyPetsViewModel : PagingViewModel
    {
        public IEnumerable<SearchPetResultViewModel> MyPets { get; set; }
    }
}
