namespace PetGuide.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;

    public class PostDetailsViewModel : BasePostViewModel
    {
        public string Id { get; set; }

        public int Likes { get; set; }

        public PostCategory Category { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
