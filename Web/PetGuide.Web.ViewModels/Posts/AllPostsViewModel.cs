namespace PetGuide.Web.ViewModels.Posts
{
    using PetGuide.Data.Models;
    using PetGuide.Services.Mapping;

    public class AllPostsViewModel : BasePostViewModel, IMapFrom<Post>
    {
        public string Id { get; set; }
    }
}
