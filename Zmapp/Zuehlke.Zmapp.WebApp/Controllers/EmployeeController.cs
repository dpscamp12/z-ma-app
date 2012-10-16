using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zuehlke.Zmapp.Services;
using Zuehlke.Zmapp.WebApp.Models;

namespace Zuehlke.Zmapp.WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Identifiers()
        {
            var employees = Repository.Instance.GetEmployees();
            var employeeIdentifiers = employees.Select(e => new { e.Id, e.FirstName, e.LastName });
            return Json(employeeIdentifiers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Employee(int id)
        {
            Employee employee = Repository.Instance.GetEmployee(id);

            EmployeeViewModel employeeViewModel = ConvertToViewModel(employee);
            return Json(employeeViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Employee(EmployeeViewModel employeeViewModel)
        {
            Employee employee = ConvertToDomainEntity(employeeViewModel);
            Repository.Instance.SetEmployee(employee);

            return Json(employeeViewModel);
        }

        [HttpDelete]
        public HttpStatusCodeResult DeleteEmployee(int id)
        {
            bool success = Repository.Instance.RemoveEmployee(id);
            return new HttpStatusCodeResult(success ? 200 : 400);  // does this make sense? 
        }

         private static ReservationViewModel ConvertToViewModel(Reservation reservation)
         {
             var reservationViewModel = new ReservationViewModel
             {
                 Start = reservation.Start,
                 End = reservation.End,
                 CustomerName = ""
             };

             if (reservation.CustomerId != null)
             {
                 IEnumerable<Customer> customers = Repository.Instance.GetCustomers();
                 Customer customer = customers.FirstOrDefault(c => c.Id == reservation.CustomerId);
                 if (customer == null)
                 {
                     throw new ArgumentException("Unknown customer Id");
                 }
                 reservationViewModel.CustomerName = customer.Name;
             }

             return reservationViewModel;
         }

         private static Reservation ConvertToDomainEntity(ReservationViewModel reservationViewModel)
         {
             var reservation = new Reservation
                                   {
                                       Start = reservationViewModel.Start,
                                       End = reservationViewModel.End
                                   };

             if (!String.IsNullOrEmpty(reservationViewModel.CustomerName))
             {
                 IEnumerable<Customer> customers = Repository.Instance.GetCustomers();
                 Customer customer = customers.FirstOrDefault(
                     c => String.Compare(c.Name, reservationViewModel.CustomerName, StringComparison.InvariantCultureIgnoreCase) == 0);
                 if (customer == null)
                 {
                     throw new ArgumentException("Invalid customer name" + reservationViewModel.CustomerName);
                 }
                 reservation.CustomerId = customer.Id;
             }

             return reservation;
         }

        private static EmployeeViewModel ConvertToViewModel(Employee employee)
        {
            var reservationViewModels = new List<ReservationViewModel>();
            if (employee.Reservations != null)
            {
                IEnumerable<ReservationViewModel> reservations = employee.Reservations.Select(ConvertToViewModel);
                reservationViewModels.AddRange(reservations);
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

         private static Employee ConvertToDomainEntity(EmployeeViewModel employeeViewModel)
         {
             var reservations = new List<Reservation>();
             if (employeeViewModel.Reservations != null)
             {
                 var domainReservations = employeeViewModel.Reservations.Select(ConvertToDomainEntity);
                 reservations.AddRange(domainReservations);
             }
             return new Employee
             {
                 Id = employeeViewModel.Id,
                 FirstName = employeeViewModel.FirstName,
                 LastName = employeeViewModel.LastName,
                 Street = employeeViewModel.Street,
                 ZipCode = employeeViewModel.ZipCode,
                 City = employeeViewModel.City,
                 Phone = employeeViewModel.Phone,
                 EMail = employeeViewModel.EMail,
                 Skills = employeeViewModel.Skills == null ? null : employeeViewModel.Skills.ToArray(),
                 CareerLevel = employeeViewModel.CareerLevel,
                 Reservations = reservations.ToArray()
             };
         }
    }
}
