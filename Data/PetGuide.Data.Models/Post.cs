namespace PetGuide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;

    public class Post : BaseDeletableModel<string>
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        // List of User Likes
        public int Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
