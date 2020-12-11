namespace PetGuide.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class AddPostInputModel
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2500)]
        public string Content { get; set; }

        public PostCategory Category { get; set; }


    }
}
