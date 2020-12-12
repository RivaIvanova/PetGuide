namespace PetGuide.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;
    using PetGuide.Web.ViewModels.Posts;

    public class PostService : IPostService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Tag> tagsRepository;

        public PostService(
            IDeletableEntityRepository<Post> postsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Tag> tagsRepository)
        {
            this.postsRepository = postsRepository;
            this.usersRepository = usersRepository;
            this.tagsRepository = tagsRepository;
        }

        // Add Post
        public async Task AddAsync(PostInputModel input, string userId)
        {
            var post = new Post
            {
                Title = input.Title,
                Content = input.Content,
                Category = input.Category,
                AuthorId = userId,
            };

            this.AddTagsToPost(input, post);

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
        }

        // Get All Posts
        public IEnumerable<AllPostsViewModel> GetAll()
        {
            return this.postsRepository
                .AllAsNoTracking()
                .Select(x => new AllPostsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Category = x.Category,
                    CreatedOn = x.CreatedOn,
                    Author = x.Author,
                })
                .ToList();
        }

        // Get All Posts By Tag
        public IEnumerable<AllPostsViewModel> GetAllByTag(int tagId)
        {
            var tag = this.tagsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == tagId);

            return this.postsRepository
                .AllAsNoTracking()
                .Where(x => x.Tags.Any(x => x.Tag.Name == tag.Name))
                .Select(x => new AllPostsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Category = x.Category,
                    CreatedOn = x.CreatedOn,
                    Author = x.Author,
                })
                .ToList();
        }

        // Get All Posts By Category
        public IEnumerable<AllPostsViewModel> GetAllByCategory(int category)
        {
            PostCategory postcat = (PostCategory)category;

            return this.postsRepository
                .AllAsNoTracking()
                .Where(x => x.Category == postcat)
                .Select(x => new AllPostsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Category = x.Category,
                    CreatedOn = x.CreatedOn,
                    Author = x.Author,
                })
                .ToList();
        }

        // Get Post Details View
        public PostDetailsViewModel GetPostDetails(string id)
        {
            var post = this.postsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            var author = this.usersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == post.AuthorId);
            var allTags = this.tagsRepository.AllAsNoTracking().ToList();
            var allCategories = Enum.GetValues(typeof(PostCategory))
                .Cast<PostCategory>()
                .ToList();
            var postTags = this.tagsRepository.AllAsNoTracking().Where(x => x.PostId == id).ToList();

            var viewModel = new PostDetailsViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Comments = post.Comments,
                CreatedOn = post.CreatedOn,
                Likes = post.Likes,
                Category = post.Category,
                Tags = postTags,
                Author = author,
                AllTags = allTags,
                AllCategories = allCategories,
            };

            return viewModel;
        }

        // Get Post Edit View
        public PostInputModel GetPostEdit(string id)
        {
            var post = this.GetPostById(id);
            var tags = this.tagsRepository.All().Where(x => x.PostId == id).Select(x => x.Name).ToList();

            var viewModel = new PostInputModel
            {
                Title = post.Title,
                Category = post.Category,
                Content = post.Content,
                Tags = string.Join(' ', tags),
            };

            return viewModel;
        }

        // Edit Post
        public async Task EditAsync(string id, PostInputModel input)
        {
            var post = this.GetPostById(id);

            post.Title = input.Title;
            post.Category = input.Category;
            post.Content = input.Content;

            this.AddTagsToPost(input, post);

            await this.postsRepository.SaveChangesAsync();
        }

        // Delete Post
        public async Task DeleteAsync(string id)
        {
            var post = this.GetPostById(id);
            this.postsRepository.Delete(post);
            await this.postsRepository.SaveChangesAsync();
        }

        public Post GetPostById(string id)
        {
            return this.postsRepository.All().FirstOrDefault(x => x.Id == id);
        }

        private void AddTagsToPost(PostInputModel input, Post post)
        {
            var tags = input.Tags.Trim().Split(' ').ToList();

            foreach (var tagName in tags)
            {
                var tag = this.tagsRepository.All().FirstOrDefault(x => x.Name == tagName);

                if (tag == null)
                {
                    tag = new Tag { Name = tagName, Post = post };
                }

                if (post.Id != tag.PostId)
                {
                    post.Tags.Add(new PostTag
                    {
                        Tag = tag,
                    });
                }
            }
        }
    }
}
