namespace PetGuide.Services.Data.Posts
{
    using System.Collections.Generic;

    using PetGuide.Web.ViewModels.Posts;

    public interface IGetAllPostsService
    {
        ICollection<AllPostsViewModel> GetAll();
    }
}
