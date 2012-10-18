using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.Services.Contracts.Employee;
using Zuehlke.Zmapp.Services.Customers;
using Zuehlke.Zmapp.Services.Employees;
using Zuehlke.Zmapp.WebApp.Models;

namespace Zuehlke.Zmapp.WebApp.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IEmployeeService employeeService;
		private readonly ICustomerService customerService;

		public EmployeeController()
			: this(new EmployeeService(), new CustomerService())
		{
		}

		public EmployeeController(IEmployeeService employeeService, ICustomerService customerService)
		{
			if (employeeService == null) throw new ArgumentNullException("employeeService");

			this.employeeService = employeeService;
			this.customerService = customerService;
		}

		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public JsonResult Identifiers()
		{
			IEnumerable<EmployeeInfo> employees = this.employeeService.GetEmployees();
			var employeeIdentifiers = employees.Select(e => new { e.Id, e.FirstName, e.LastName });

			return Json(employeeIdentifiers, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult Employee(int id)
		{
			EmployeeInfo employee = this.employeeService.GetEmployee(id);
			IEnumerable<ReservationInfo> reservations = this.employeeService.GetReservationsOfEmployee(employee.Id);

			EmployeeViewModel employeeViewModel = this.ConvertToViewModel(employee, reservations);
			return Json(employeeViewModel, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult Employee(EmployeeViewModel employeeViewModel)
		{
			EmployeeInfo employee = ConvertToDataObject(employeeViewModel);
			this.employeeService.SetEmployee(employee);

			if (employeeViewModel.Reservations != null)
			{
				ReservationInfo[] reservations = employeeViewModel.Reservations
					.Select(this.ConvertToDataObject)
					.ToArray();
				this.employeeService.SetReservationsOfEmployee(employee.Id, reservations);
			}

			return Json(employeeViewModel);
		}

		[HttpDelete]
		public HttpStatusCodeResult DeleteEmployee(int id)
		{
			bool success = this.employeeService.RemoveEmployee(id);
			return new HttpStatusCodeResult(success ? 200 : 400);  // does this make sense? 
		}

		private ReservationViewModel ConvertToViewModel(ReservationInfo reservation)
		{
			var reservationViewModel = new ReservationViewModel
			{
				Start = reservation.Start,
				End = reservation.End,
				CustomerName = string.Empty,
			};

			if (reservation.CustomerId != null)
			{
				IEnumerable<CustomerInfo> customers = this.customerService.GetCustomers();
				CustomerInfo customer = customers.FirstOrDefault(c => c.Id == reservation.CustomerId);
				if (customer == null)
				{
					throw new ArgumentException("Unknown customer Id");
				}
				reservationViewModel.CustomerName = customer.Name;
			}

			return reservationViewModel;
		}

		private ReservationInfo ConvertToDataObject(ReservationViewModel reservationViewModel)
		{
			var reservation = new ReservationInfo
								  {
									  Start = reservationViewModel.Start,
									  End = reservationViewModel.End
								  };

			if (!String.IsNullOrEmpty(reservationViewModel.CustomerName))
			{
				IEnumerable<CustomerInfo> customers = this.customerService.GetCustomers();
				CustomerInfo customer = customers.FirstOrDefault(
					c => String.Compare(c.Name, reservationViewModel.CustomerName, StringComparison.InvariantCultureIgnoreCase) == 0);
				if (customer == null)
				{
					throw new ArgumentException("Invalid customer name" + reservationViewModel.CustomerName);
				}
				reservation.CustomerId = customer.Id;
			}

			return reservation;
		}

		private EmployeeViewModel ConvertToViewModel(EmployeeInfo employee, IEnumerable<ReservationInfo> reservations)
		{
			var reservationViewModels = new List<ReservationViewModel>();
			if (reservations != null)
			{
				IEnumerable<ReservationViewModel> reservationsViewModels = reservations.Select(this.ConvertToViewModel);
				reservationViewModels.AddRange(reservationsViewModels);
			}

			return new EmployeeViewModel
					   {
						   Id = employee.Id,
						   FirstName = employee.FirstName,
						   LastName = employee.LastName,
						   Street = employee.Street,
						   ZipCode = employee.ZipCode,
						   City = employee.City,
						   Phone = employee.Phone,
						   EMail = employee.EMail,
						   Skills = employee.Skills == null ? null : employee.Skills.ToArray(),
						   CareerLevel = employee.CareerLevel,
						   Reservations = reservationViewModels.ToArray()
					   };
		}

		private static EmployeeInfo ConvertToDataObject(EmployeeViewModel employeeViewModel)
		{
			var e = new EmployeeInfo
			{
				Id = employeeViewModel.Id,
				FirstName = employeeViewModel.FirstName,
				LastName = employeeViewModel.LastName,
				Street = employeeViewModel.Street,
				ZipCode = employeeViewModel.ZipCode,
				City = employeeViewModel.City,
				Phone = employeeViewModel.Phone,
				EMail = employeeViewModel.EMail,
				CareerLevel = employeeViewModel.CareerLevel,
				Skills = employeeViewModel.Skills
			};

			return e;
		}
	}
}