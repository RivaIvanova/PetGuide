namespace PetGuide.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum District
    {
        Center = 1,
        Mladost = 2,

        [Display(Name = "Studentski grad")]
        StudentskiGrad = 3,
        Hladilnika = 4,

        [Display(Name = "Ovcha Kupel")]
        OvchaKupel = 5,
    }
}
