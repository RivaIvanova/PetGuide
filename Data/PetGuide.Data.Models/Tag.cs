namespace PetGuide.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.Posts = new HashSet<PostTag>();
        }

        [RegularExpression(@"^([a-zA-z]{2,})$")]
        public string Name { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }

        public ICollection<PostTag> Posts { get; set; }
    }
}
