namespace PetGuide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;
    using PetGuide.Data.Models.Enums;

    public class Post : BaseDeletableModel<string>
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
            this.Tags = new HashSet<PostTag>();
            this.Pictures = new HashSet<Picture>();
        }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2500)]
        public string Content { get; set; }

        [Required]
        public PostCategory Category { get; set; }

        // List of User Likes
        public int Likes { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<PostTag> Tags { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
