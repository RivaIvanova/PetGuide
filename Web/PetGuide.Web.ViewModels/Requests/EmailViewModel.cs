namespace PetGuide.Web.ViewModels.Requests
{
    public class EmailViewModel
    {
        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public string ReceiverEmail { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
