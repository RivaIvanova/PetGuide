namespace PetGuide.Web.ViewModels.Pets
{
    using System.Collections.Generic;

    public class AllPetsListViewModel : PagingViewModel
    {
        public IEnumerable<AllPetsViewModel> Pets { get; set; }
    }
}
