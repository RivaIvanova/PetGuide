namespace PetGuide.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class AllPostsInCategoryViewModel
    {
        public string Category { get; set; }

        public IEnumerable<AllPostsViewModel> Posts { get; set; }
    }
}
