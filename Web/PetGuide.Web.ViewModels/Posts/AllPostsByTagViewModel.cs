namespace PetGuide.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class AllPostsByTagViewModel
    {
        public string Tag { get; set; }

        public IEnumerable<AllPostsViewModel> Posts { get; set; }
    }
}
