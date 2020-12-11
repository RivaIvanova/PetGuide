namespace PetGuide.Web.ViewModels.Posts
{
    using System;

    using PetGuide.Data.Models;

    public abstract class BasePostViewModel
    {
        public Picture Picture { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("f");

        public string Content { get; set; }

        public string ShortContent => this.Content.Length >= 100 ? $"{this.Content.Substring(0, 100)}..." : this.Content;

        public ApplicationUser Author { get; set; }

    }
}
