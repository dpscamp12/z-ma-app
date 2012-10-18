using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using Zuehlke.Zmapp.Services.Client;
using Zuehlke.Zmapp.Services.Contracts.Customers;

using Zuehlke.Zmapp.Services.Contracts.Employees;
using Zuehlke.Zmapp.WebApp.Models;

namespace Zuehlke.Zmapp.WebApp.Controllers
{
	public class SearchController : Controller
	{
		private readonly ICustomerService customerService;
		private readonly IEmployeeReservationService reservationService;

		public SearchController()
			: this(new CustomerServiceProxy(), new EmployeeReservationServiceProxy())
		{
		}

		public SearchController(ICustomerService customerService, IEmployeeReservationService reservationService)
		{
			if (customerService == null) throw new ArgumentNullException("customerService");
			if (reservationService == null) throw new ArgumentNullException("reservationService");

			this.customerService = customerService;
			this.reservationService = reservationService;
		}

		[HttpGet]
		public ActionResult Index()
		{
			var model = new SearchParamViewModel
			{
				StartDate = DateTime.Now + TimeSpan.FromDays(1),
				EndDate = DateTime.Now + TimeSpan.FromDays(90),
			};

			IEnumerable<CustomerInfo> customers = this.customerService.GetCustomers();

			ViewData["Customers"] = new SelectList(customers, "Id", "Name");
			ViewData["Skills"] = GetSkillSelectList(model.SelectedSkills);
			ViewData["CareerLevels"] = GetCareerLevelsSelectList(model.SelectedCareerLevels);

			return View(model);
		}

		[HttpPost]
		public ActionResult Result(SearchParamViewModel searchParams)
		{
			if (!ModelState.IsValid)
			{
				IEnumerable<CustomerInfo> customers = this.customerService.GetCustomers();

				// send back search-view with validation errors.
				ViewData["Customers"] = new SelectList(customers, "Id", "Name");
				ViewData["Skills"] = GetSkillSelectList(searchParams.SelectedSkills);
				ViewData["CareerLevels"] = GetCareerLevelsSelectList(searchParams.SelectedCareerLevels);
				return View("Index", searchParams);
			}

			IEnumerable<EmployeeSearchResult> employees = SearchEmployees(searchParams);
			IEnumerable<EmployeeIdentifierViewModel> employeeIdentifiers = employees.Select(
				e => new EmployeeIdentifierViewModel
				{
					EmployeeId = e.Id,
					EmployeeName = e.EmployeeName
				});

			ViewData["SearchParamViewModel"] = searchParams;
			return View("Result", employeeIdentifiers);
		}

		[HttpPost]
		public ActionResult BookEmployee(int employeeId, [Deserialize]SearchParamViewModel searchParam)
		{
			this.reservationService.ReserveEmployeeForCustomer(
				employeeId,
				searchParam.SelectedCustomer,
				searchParam.StartDate,
				 searchParam.EndDate);
			return Result(searchParam);
		}

		private IEnumerable<EmployeeSearchResult> SearchEmployees(SearchParamViewModel searchParamViewModel)
		{
			var query = new EmployeeQuery
			{
				BeginOfWorkPeriod = searchParamViewModel.StartDate,
				EndOfWorkPeriod = searchParamViewModel.EndDate,
				CustomerId = searchParamViewModel.SelectedCustomer,
				RequestedCareerLevels = searchParamViewModel.SelectedCareerLevels.Select(i => (CareerLevel)i).ToArray(),
				RequestedSkills = searchParamViewModel.SelectedSkills.Select(i => (Skill)i).ToArray()
			};

			return this.reservationService.FindPotentialEmployeesForCustomer(query);
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