namespace PetGuide.Web.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Web.Infrastructure.Attributes;

    public class EditEventInputModel : BaseEventInputModel
    {
        [Required]
        [DateMinValue(0)]
        public DateTime Date { get; set; }
    }
}
