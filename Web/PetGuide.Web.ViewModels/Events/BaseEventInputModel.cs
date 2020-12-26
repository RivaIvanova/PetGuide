namespace PetGuide.Web.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Models;
    using PetGuide.Web.Infrastructure.Attributes;

    public class BaseEventInputModel
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(RegexValidation.Event.Name, ErrorMessage = RegexValidation.Event.NameErrorMessage)]
        public string Name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "The purpose should be at least 10 characters long.")]
        [RegularExpression(RegexValidation.Event.Purpose, ErrorMessage = RegexValidation.Event.PurposeErrorMessage)]
        public string Purpose { get; set; }

        [Required]
        [MaxLength(1500)]
        [RegularExpression(RegexValidation.Event.Description, ErrorMessage = RegexValidation.Event.DescriptionErrorMessage)]
        public string Description { get; set; }

        [Required]
        [MaxLength(1500)]
        [RegularExpression(RegexValidation.Event.Activities, ErrorMessage = RegexValidation.Event.ActivitiesErrorMessage)]
        public string Activities { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public Location Location { get; set; }
    }
}
