﻿namespace PetGuide.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using PetGuide.Web.Infrastructure.Attributes;

    public class EventInputModel : BaseEventInputModel
    {
        [Required]
        [DateMinValue(3)]
        public DateTime Date { get; set; }

        [Required]
        [PicturesMaxCount(5)]
        public ICollection<IFormFile> Pictures { get; set; }
    }
}
