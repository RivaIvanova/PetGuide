namespace PetGuide.Web.ViewModels.Pets
{
    using System.Collections.Generic;

    public class SearchPetListViewModel : BasePetViewModel
    {
        public IEnumerable<SearchPetViewModel> Pets { get; set; }
    }
}
