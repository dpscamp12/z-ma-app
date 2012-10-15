using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Zuehlke.Zmapp.Services;
using Zuehlke.Zmapp.Services.Contracts.Employee;
using Zuehlke.Zmapp.WebApp.Controllers;

namespace Zuehlke.Zmapp.WebApp.Models
{
 


    public class FoundEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SearchViewModel
    {
        [DisplayName("Customer:")]
        [Required(ErrorMessage = "Please select a customer")]
        public int SelectedCustomer { get; set; }

        [DisplayName("Start of Period:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Start date is required")]
        [DateStart(ErrorMessage = "Start date must be in future.")]
        public DateTime StartDate { get; set; }

        [DisplayName("End of Period:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "End of period is required.")]
        [DateEnd(DateStartProperty = "StartDate", ErrorMessage = "End of period must be after start of period.")]
        public DateTime EndDate { get; set; }

        [DisplayName("Requested ski lls:")]
        [Required(ErrorMessage = "Please select at least one skill.")]
        public int[] SelectedSkills { get; set; }

        public SelectList Customers { get; set; }

        public MultiSelectList Skills
        {
            get
            {
                IEnumerable<Skill> values = Enum.GetValues(typeof(Skill)).Cast<Skill>();
                IEnumerable keyedValues = values.Select(value => new { Id = (int)value, Name = value });
                return new SelectList(keyedValues, "Id", "Name", SelectedSkills);
            }
        }
    }

    public class BookEmployeeViewModel
    {
        public bool CanBeBooked { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CareerLevel CareerLevel { get; set; }

        public Skill[] Skills { get; set; }
    }

    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CareerLevel CareerLevel { get; set; }

        public Skill[] Skills { get; set; }
    }
}
