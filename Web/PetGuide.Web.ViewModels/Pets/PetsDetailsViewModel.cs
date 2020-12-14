﻿namespace PetGuide.Web.ViewModels.Pets
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PetGuide.Data.Models;

    public class PetsDetailsViewModel : BasePetViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("f");

        public ApplicationUser Contact { get; set; }
    }
}
