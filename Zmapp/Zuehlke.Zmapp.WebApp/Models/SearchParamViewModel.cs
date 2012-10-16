using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zuehlke.Zmapp.WebApp.Controllers;

namespace Zuehlke.Zmapp.WebApp.Models
{
    public class SearchParamViewModel
    {
        [DisplayName("Current customer:")]
        [Required(ErrorMessage = "Please select a customer")]
        public int SelectedCustomer { get; set; }

        [DisplayName("Start of period:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Start date is required")]
        [DateStart(ErrorMessage = "Start date must be in future.")]
        public DateTime StartDate { get; set; }

        [DisplayName("End of period:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "End of period is required.")]
        [DateEnd(DateStartProperty = "StartDate", ErrorMessage = "End of period must be after start of period.")]
        public DateTime EndDate { get; set; }

        [DisplayName("Requested skills:")]
        [Required(ErrorMessage = "Please select at least one skill.")]
        public int[] SelectedSkills { get; set; }

        [DisplayName("Requested career Levels:")]
        [Required(ErrorMessage = "Please select at least one career level.")]
        public int[] SelectedCareerLevels { get; set; }
    }
}