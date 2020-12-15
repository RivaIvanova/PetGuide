namespace PetGuide.Web.Infrastructure.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateMinValue : ValidationAttribute
    {
        private readonly DateTime minDate = DateTime.UtcNow.AddDays(3);

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                if (date >= this.minDate)
                {
                    return true;
                }
            }

            this.ErrorMessage = $"Date should be not earlier than {this.minDate}.";
            return false;
        }
    }
}
