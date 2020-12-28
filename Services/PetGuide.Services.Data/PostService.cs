namespace PetGuide.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;
    using PetGuide.Services.Mapping;
    using PetGuide.Web.ViewModels.Pictures;
    using PetGuide.Web.ViewModels.Posts;

    public class PostService : IPostService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Tag> tagsRepository;
        private readonly IPictureService picturesService;

        public PostService(
            IDeletableEntityRepository<Post> postsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Tag> tagsRepository,
            IPictureService picturesService)
        {
            this.postsRepository = postsRepository;
            this.usersRepository = usersRepository;
            this.tagsRepository = tagsRepository;
            this.picturesService = picturesService;
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

            await this.picturesService.Upload(
                input.Pictures.Select(i => new PictureInputModel
                {
                    Name = i.FileName,
                    Type = i.ContentType,
                    Content = i.OpenReadStream(),
                }),
                userId,
                post.Id);
        }

        // Get All Posts
        public IEnumerable<AllPostsViewModel> GetAll()
        {
            return this.postsRepository
                .AllAsNoTracking()
                .To<AllPostsViewModel>()
                .ToList();
        }

        // Get All Posts By Tag
        public AllPostsByTagViewModel GetPostsByTag(int id)
        {
            var tag = this.GetTagById(id);
            var posts = this.postsRepository
                .AllAsNoTracking()
                .Where(x => x.Tags.Any(x => x.TagId == id))
                .To<AllPostsViewModel>()
                .ToList();

            var viewModel = new AllPostsByTagViewModel
            {
                Tag = tag.Name,
                Posts = posts,
            };

            return viewModel;
        }

        // Get Post Details View
        public PostDetailsViewModel GetPostDetails(string id)
        {
            var post = this.postsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            var author = this.usersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == post.AuthorId);
            var allTags = this.tagsRepository.AllAsNoTracking().ToList();
            var postTags = this.tagsRepository.AllAsNoTracking().Where(x => x.PostId == id).ToList();
            var categories = this.GetCategoriesWithPostsCount();
            var pictures = this.picturesService.GetPicturesToShow(id);

            var viewModel = new PostDetailsViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedOn = post.CreatedOn,
                Category = post.Category,
                Tags = postTags,
                Author = author,
                AllTags = allTags,
                AllCategories = categories,
                FirstPictureToShow = pictures.FirstOrDefault(),
                PicturesToShow = pictures,
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

        // Get Post By Id
        public Post GetPostById(string id)
        {
            return this.postsRepository.All().FirstOrDefault(x => x.Id == id);
        }

        // Get Tag By Id
        public Tag GetTagById(int id)
        {
            return this.tagsRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public int GetCount()
        {
            return this.postsRepository.All().Count();
        }

        // Private Methods
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

        private Dictionary<PostCategory, int> GetCategoriesWithPostsCount()
        {
            var allCategories = Enum.GetValues(typeof(PostCategory))
                            .Cast<PostCategory>()
                            .ToList();

            var categories = new Dictionary<PostCategory, int>();

            foreach (var cat in allCategories)
            {
                var posts = this.postsRepository.AllAsNoTracking().Where(x => x.Category == cat).Count();
                categories.Add(cat, posts);
            }

            return categories;
        }
    }
}
