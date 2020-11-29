namespace PetGuide.Web.ViewModels.Posts
{
    using System;

    using PetGuide.Data.Models;

    public class AllPostsViewModel
    {
        public string Id { get; set; }

        public Picture Picture { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("f");

        public string Content { get; set; }

        public string ShortContent => this.Content.Length >= 200 ? $"{this.Content.Substring(0, 200)}..." : this.Content;

        public ApplicationUser Author { get; set; }
    }
}
