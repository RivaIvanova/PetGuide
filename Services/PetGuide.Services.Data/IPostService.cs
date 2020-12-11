namespace PetGuide.Services.Data
{
    using System.Collections.Generic;

    using PetGuide.Web.ViewModels.Posts;

    public interface IPostService
    {
        IEnumerable<AllPostsViewModel> GetAll();

        PostDetailsViewModel GetPostDetails(string id);
    }
}
