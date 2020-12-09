namespace PetGuide.Services.Data.Posts
{
    using System.Collections.Generic;
    using System.Linq;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Posts;

    public class GetAllPostsService : IGetAllPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public GetAllPostsService(
            IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public ICollection<AllPostsViewModel> GetAll()
        {
            return this.postsRepository.AllAsNoTracking()
                .Select(x => new AllPostsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    Author = x.Author,
                })
                .ToList();
        }
    }
}
