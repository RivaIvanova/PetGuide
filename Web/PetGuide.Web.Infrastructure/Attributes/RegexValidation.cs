namespace PetGuide.Web.Infrastructure.Attributes
{
    public static class RegexValidation
    {
        public static class User
        {
            public const string PersonalName = @"^([A-Z][a-z]+)(-([A-Z][a-z]+))?$";
            public const string PersonalNameErrorMessage = "Name should start with capital letter and shouldn't contain multiple whitespaces.";
            public const string Description = @"^(([A-Za-z0-9!?@#<>=.,-]\s?)+)$";
            public const string DescriptionErrorMessage = "Pet description shouldn't contain multiple whitespaces. Allowed characters are 0-9!?@#<>=.-";
        }

        public static class Pet
        {
            public const string Name = @"^[A-Z][a-z]+\s?(([A-Z][a-z]+)\s?){0,3}$";
            public const string NameErrorMessage = "Name should start with capital letter and could be four parts.";
            public const string Description = @"^(([A-Za-z0-9!?@#<>=.,-]\s?)+)$";
            public const string DescriptionErrorMessage = "Pet description shouldn't contain multiple whitespaces. Allowed characters are 0-9!?@#<>=.-";
        }

        public static class Post
        {
            public const string Title = @"^(([A-Za-z0-9!?@#<>=.-]\s?)+)$";
            public const string TitleErrorMessage = "Post title shouldn't contain multiple whitespaces. Allowed characters are 0-9!?@#<>=.-";
            public const string Content = @"^(([A-Za-z0-9!?@#<>=.,-]\s?)+)$";
            public const string ContentErrorMessage = "Post content shouldn't contain multiple whitespaces. Allowed characters are 0-9!?@#<>=.-";
            public const string Tags = @"^(?:[a-zA-z]{2,}\s?)*$";
            public const string TagsErrorMessage = "Tag should contain only letters and be separated by whitespace.";
        }

        public static class Event
        {
            public const string Name = @"^(([A-Za-z0-9!?@#<>=.-]\s?)+)$";
            public const string NameErrorMessage = "Event name shouldn't contain multiple whitespaces. Allowed characters are 0-9!?@#<>=.-";
            public const string Purpose = @"^(([A-Za-z0-9.,]\s?)+)$";
            public const string PurposeErrorMessage = "Event purpose shouldn't contain multiple whitespaces. Allowed character is .";
            public const string Description = @"^(([A-Za-z0-9!?@#<>=.,-]\s?)+)$";
            public const string DescriptionErrorMessage = "Event description shouldn't contain multiple whitespaces. Allowed characters are 0-9!?@#<>=.-";
            public const string Activities = @"^(([A-Za-z0-9.,-]\s?)+)$";
            public const string ActivitiesErrorMessage = "Event activities shouldn't contain multiple whitespaces. Allowed character is .-";
        }
    }
}
