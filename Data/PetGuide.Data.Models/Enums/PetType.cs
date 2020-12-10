namespace PetGuide.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum PetType
    {
        Cat = 1,
        Dog = 2,
        Rabbit = 3,
        Bird = 4,

        [Display(Name = "Guinea Pig")]
        GuindeaPig = 5,

        Turtle = 6,
        Other = 7,
    }
}
