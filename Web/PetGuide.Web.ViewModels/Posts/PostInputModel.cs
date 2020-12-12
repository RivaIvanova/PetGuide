namespace PetGuide.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Models.Enums;

    public class PostInputModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2500)]
        public string Content { get; set; }

        public PostCategory Category { get; set; }

        [RegularExpression(@"^(?:[a-zA-z]{2,}\s?)*$", ErrorMessage = "Tag should contain only letters and be separated by whitespace.")]
        public string Tags { get; set; }
    }
}
