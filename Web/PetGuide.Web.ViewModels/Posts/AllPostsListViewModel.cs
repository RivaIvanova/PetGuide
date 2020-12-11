namespace PetGuide.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class AllPostsListViewModel
    {
        public IEnumerable<AllPostsViewModel> Posts { get; set; }
    }
}
