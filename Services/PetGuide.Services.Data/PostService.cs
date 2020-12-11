namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Posts;

    public class PostService : IPostService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public PostService(
            IDeletableEntityRepository<Post> postsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.postsRepository = postsRepository;
            this.usersRepository = usersRepository;
        }

        public IEnumerable<AllPostsViewModel> GetAll()
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

        public PostDetailsViewModel GetPostDetails(string id)
        {
            var post = this.postsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            var author = this.usersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == post.AuthorId);

            var viewModel = new PostDetailsViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Comments = post.Comments,
                CreatedOn = post.CreatedOn,
                Likes = post.Likes,
                Category = post.Category,
                Author = author,
            };

            return viewModel;
        }
    }
}
