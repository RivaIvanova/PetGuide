namespace PetGuide.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Services.Data;
    using PetGuide.Web.ViewModels.Posts;

    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult All(string tag = "")
        {
            var posts = this.postService.GetAll();

            if (tag.Length != 0)
            {
                posts = this.postService.GetAllByTag(int.Parse(tag));
            }

            var viewModel = new AllPostsListViewModel
            {
                Posts = posts,
            };

            return this.View(viewModel);
        }

        public IActionResult Category(int category)
        {
            var viewModel = new AllPostsInCategoryViewModel
            {
                //Category = category,
                Posts = this.postService.GetAllByCategory(category),
            };

            return this.View(viewModel);
        }

        // Add Post
        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(PostInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.postService.AddAsync(input, userId);

            return this.RedirectToAction(nameof(this.All));
        }

        // Edit Post
        [Authorize]
        public IActionResult Edit(string id)
        {
            var post = this.postService.GetPostById(id);
            var authorId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (post.AuthorId != authorId)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var inputModel = this.postService.GetPostEdit(id);
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, PostInputModel input)
        {
            var post = this.postService.GetPostById(id);
            var authorId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (post.AuthorId != authorId)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.postService.EditAsync(id, input);
            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        // Delete Pets
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var post = this.postService.GetPostById(id);
            var authorId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (post.AuthorId == authorId)
            {
                await this.postService.DeleteAsync(id);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        // Get Post Details View
        public IActionResult Details(string id)
        {
            var viewModel = this.postService.GetPostDetails(id);

            return this.View(viewModel);
        }
    }
}
