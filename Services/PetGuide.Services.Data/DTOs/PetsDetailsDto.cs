﻿namespace PetGuide.Services.Data.DTOs
{
    using System;

    using PetGuide.Data.Models;

    public class PetsDetailsDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public int? Age { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description.Trim();

        public ApplicationUser Contact { get; set; }
    }
}
