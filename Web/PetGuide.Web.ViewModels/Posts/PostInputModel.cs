namespace PetGuide.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Models.Enums;

    public class PostInputModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2500)]
        public string Content { get; set; }

        public PostCategory Category { get; set; }

        [RegularExpression(@"^(?:[a-zA-z]{2,}\s?)*$", ErrorMessage = "Tag field is not valid.")]
        public string Tags { get; set; }
    }
}
