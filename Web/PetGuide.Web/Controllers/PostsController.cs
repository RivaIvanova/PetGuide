namespace PetGuide.Web.Controllers
{
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

        public IActionResult All()
        {
            var viewModel = new AllPostsListViewModel
            {
                Posts = this.postService.GetAll(),
            };

            return this.View(viewModel);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        public IActionResult Details(string id)
        {
            var viewModel = this.postService.GetPostDetails(id);

            return this.View(viewModel);
        }
    }
}
