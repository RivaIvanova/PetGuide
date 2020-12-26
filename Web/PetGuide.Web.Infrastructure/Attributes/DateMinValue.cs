namespace PetGuide.Web.Infrastructure.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateMinValue : ValidationAttribute
    {
        private readonly int days;

        public DateMinValue(int daysFromNow)
        {
            this.days = daysFromNow;
        }

        public DateTime MinDate => DateTime.UtcNow.AddDays(this.days);

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                if (date >= this.MinDate)
                {
                    return true;
                }
            }

            this.ErrorMessage = $"Date should be not earlier than {this.MinDate}.";
            return false;
        }
    }
}
