namespace PetGuide.Web.ViewModels.Posts
{
    using System.Linq;

    using AutoMapper;
    using PetGuide.Data.Models;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Pictures;

    public class AllPostsViewModel : BasePostViewModel, IMapFrom<Post>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public PictureViewModel FirstPicture { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, AllPostsViewModel>()
                .ForMember(x => x.FirstPicture, opt => opt.MapFrom(
                    x => x.Pictures
                    .Select(x => new PictureViewModel
                    {
                        Id = x.Id,
                        Name = x.Folder + "/Fullscreen_" + x.Id + ".jpg",
                    })
                    .FirstOrDefault()));
        }
    }
}
