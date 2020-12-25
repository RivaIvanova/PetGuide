namespace PetGuide.Web.Infrastructure.Attributes
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class PicturesMaxCount : ValidationAttribute
    {
        private readonly int maxCount;

        public PicturesMaxCount(int maxCount)
        {
            this.maxCount = maxCount;
        }

        public override bool IsValid(object value)
        {
            if (value is ICollection pictures)
            {
                if (pictures.Count <= this.maxCount)
                {
                    return true;
                }
            }

            this.ErrorMessage = $"Pictures should not be more than {this.maxCount}.";
            return false;
        }
    }
}
