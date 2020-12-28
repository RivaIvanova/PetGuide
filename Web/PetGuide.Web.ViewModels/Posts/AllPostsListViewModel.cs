namespace PetGuide.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class AllPostsListViewModel : PagingViewModel
    {
        public IEnumerable<AllPostsViewModel> Posts { get; set; }
    }
}
