using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using Zuehlke.Zmapp.Services;
using Zuehlke.Zmapp.Services.Contracts.Employee;
using Zuehlke.Zmapp.WebApp.Models;

namespace Zuehlke.Zmapp.WebApp.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new SearchParamViewModel
            {
                StartDate = DateTime.Now + TimeSpan.FromDays(1),
                EndDate = DateTime.Now + TimeSpan.FromDays(90),
            };

            ViewData["Customers"] = new SelectList(Repository.Instance.GetCustomers(), "Id", "Name");
            ViewData["Skills"] = GetSkillSelectList(model.SelectedSkills);
            ViewData["CareerLevels"] = GetCareerLevelsSelectList(model.SelectedCareerLevels);

            return View(model);
        }

        [HttpPost]
        public ActionResult Result(SearchParamViewModel searchParams)
        {
            if (!ModelState.IsValid)
            {
                // send back search-view with validation errors.
                ViewData["Customers"] = new SelectList(Repository.Instance.GetCustomers(), "Id", "Name");
                ViewData["Skills"] = GetSkillSelectList(searchParams.SelectedSkills);
                ViewData["CareerLevels"] = GetCareerLevelsSelectList(searchParams.SelectedCareerLevels);
                return View("Index", searchParams);
            }

            IEnumerable<Employee> employees = SearchEmployees(searchParams);
            IEnumerable<EmployeeIdentifierViewModel> employeeIdentifiers = employees.Select(
                e => new EmployeeIdentifierViewModel
                {
                    EmployeeId = e.Id,
                    EmployeeName = String.Format("{0} {1}", e.FirstName, e.LastName)
                });

            ViewData["SearchParamViewModel"] = searchParams;
            return View("Result", employeeIdentifiers);
        }

        [HttpPost]
        public ActionResult BookEmployee(int employeeId, [Deserialize]SearchParamViewModel searchParam)
        {
            var reservation = new Reservation
            {
                CustomerId = searchParam.SelectedCustomer,
                Start = searchParam.StartDate,
                End = searchParam.EndDate
            };
            BookEmployee(employeeId, reservation);
            return Result(searchParam);
        }

        private bool BookEmployee(int employeeId, Reservation reservation)
        {
            Employee employee = Repository.Instance.GetEmployee(employeeId);
            if (employee == null)
            {
                return false;
            }

            List<Reservation> reservations = employee.Reservations == null
                                                 ? new List<Reservation>()
                                                 : employee.Reservations.ToList();
            reservations.Add(reservation);
            employee.Reservations.AddRange(reservations);
            Repository.Instance.SetEmployee(employee);
            return true;
        }

        private IEnumerable<Employee> SearchEmployees(SearchParamViewModel searchParamViewModel)
        {
            var evaluationService = new EmployeeEvaluationService();
            var query = new EmployeeQuery
            {
                BeginOfWorkPeriod = searchParamViewModel.StartDate,
                EndOfWorkPeriod = searchParamViewModel.EndDate,
                CustomerId = searchParamViewModel.SelectedCustomer,
                RequestedCareerLevels = searchParamViewModel.SelectedCareerLevels.Select(i => (CareerLevel)i).ToArray(),
                RequestedSkills = searchParamViewModel.SelectedSkills.Select(i => (Skill)i).ToArray()
            };

            return evaluationService.FindEmployees(query);
        }

        private static MultiSelectList GetSkillSelectList(int[] selectedSkillIds)
        {
            IEnumerable<Skill> values = Enum.GetValues(typeof(Skill)).Cast<Skill>();
            IEnumerable keyedValues = values.Select(value => new { Id = (int)value, Name = value });
            return new SelectList(keyedValues, "Id", "Name", selectedSkillIds);
        }

        private static MultiSelectList GetCareerLevelsSelectList(int[] selectedCareerLevelIds)
        {
            IEnumerable<CareerLevel> values = Enum.GetValues(typeof(CareerLevel)).Cast<CareerLevel>();
            IEnumerable keyedValues = values.Select(value => new { Id = (int)value, Name = value });
            return new SelectList(keyedValues, "Id", "Name", selectedCareerLevelIds);
        }
    }
}
