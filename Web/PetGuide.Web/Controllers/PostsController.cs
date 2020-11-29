namespace PetGuide.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetGuide.Services.Data.Posts;

    public class PostsController : Controller
    {
        private readonly IGetAllPostsService getAllPostsService;

        public PostsController(IGetAllPostsService getAllPostsService)
        {
            this.getAllPostsService = getAllPostsService;
        }

        public IActionResult All()
        {
            var posts = this.getAllPostsService.GetAll();

            return this.View(posts);
        }
    }
}
