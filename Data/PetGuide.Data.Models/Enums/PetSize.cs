namespace PetGuide.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum PetSize
    {
        [Display(Name = "Under 5kg")]
        Tiny = 1,

        [Display(Name = "Between 5 and 15kg")]
        Small = 2,

        [Display(Name = "Between 16 and 25kg")]
        Medium = 3,

        [Display(Name = "Between 26 and 45kg")]
        Big = 4,

        [Display(Name = "Between 46 and 65kg")]
        Large = 5,

        [Display(Name = "More than 65kg")]
        Huge = 6,
    }
}
