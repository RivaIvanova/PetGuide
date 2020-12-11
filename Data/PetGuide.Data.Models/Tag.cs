namespace PetGuide.Data.Models
{
    using PetGuide.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }
    }
}
