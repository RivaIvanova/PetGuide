namespace PetGuide.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    using PetGuide.Data.Models;
    using PetGuide.Data.Models.Enums;

    public class PostDetailsViewModel : BasePostViewModel
    {
        public string Id { get; set; }

        public int Likes { get; set; }

        public string Contribution => this.Likes == 1 ? $"{this.Likes} person found this article useful." : $"{this.Likes} people found this article useful.";

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<Tag> AllTags { get; set; }

        public IDictionary<PostCategory, int> AllCategories { get; set; }
    }
}
