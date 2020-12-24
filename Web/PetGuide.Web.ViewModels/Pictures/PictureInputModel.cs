namespace PetGuide.Web.ViewModels.Pictures
{
    using System.IO;

    public class PictureInputModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Stream Content { get; set; }
    }
}
