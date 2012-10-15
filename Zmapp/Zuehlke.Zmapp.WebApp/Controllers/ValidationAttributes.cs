using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Zuehlke.Zmapp.WebApp.Controllers
{
    public sealed class DateStartAttribute : ValidationAttribute
    {
        // Reservation must start in the future
        public override bool IsValid(object value)
        {
            if (ReferenceEquals(value, null))
            {
                return false;
            }
            return ((DateTime)value > DateTime.Now);
        }
    }

    public sealed class DateEndAttribute : ValidationAttribute
    {
        public string DateStartProperty { get; set; }

        // Reservation start time must be before the end time
        public override bool IsValid(object value)
        {
            if (ReferenceEquals(value, null))
            {
                return false;
            }
            var dateEnd = (DateTime) value;

            string dateStartString = HttpContext.Current.Request[DateStartProperty];
            DateTime dateStart;
            if (!DateTime.TryParse(dateStartString, out dateStart))
            {
                return false;
            }

            return dateStart <= dateEnd;
        }
    }
}