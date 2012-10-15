using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Zuehlke.Zmapp.Services;
using Zuehlke.Zmapp.Services.Contracts.Employee;
using Zuehlke.Zmapp.WebApp.Models;

namespace Zuehlke.Zmapp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Employees()
        {
            var employees = Repository.Instance.GetEmployees();
            var foundEmployees = employees.Select(e => new FoundEmployeeViewModel { Id = e.Id, Name = String.Format("{0} {1}", e.FirstName, e.LastName) });
            return View(foundEmployees);
        }

        [HttpGet]
        public JsonResult Employee(int id)
        {
            var employee = Repository.Instance.GetEmployee(id);

            employee.Skills = new [] { Skill.SqlServer, Skill.CSharp };
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Employee(Employee text)
        {
            if (ModelState.IsValid)
            Thread.Sleep(1000);
            //  throw new ArgumentException("Bad things happended");
            return Json("Hallo");
        }        


        [HttpGet]
        public ActionResult Search()
        {
            var employees = Repository.Instance.GetEmployees();
            var resultModel = employees.Select(e => new FoundEmployeeViewModel { Id = e.Id, Name = String.Format("{0} {1}", e.FirstName, e.LastName) });
            return View("SearchResult", resultModel);
        }

        //[HttpGet]
        //public ActionResult EmployeeDetail(int id)
        //{
        //    Employee emp = Repository.Instance.GetEmployee(id);
        //    var viewModel = new BookEmployeeViewModel
        //                        {
        //                            Id = emp.Id,
        //                            FirstName = emp.FirstName,
        //                            LastName = emp.LastName,
        //                            CareerLevel = emp.CareerLevel,
        //                            Skills = emp.Skills
        //                        };
        //    return PartialView(viewModel);
        //}
        //[HttpGet]
        //public ActionResult Search()
        //{
        //    var model = new SearchViewModel
        //                    {
        //                        StartDate = DateTime.Now,
        //                        EndDate = DateTime.Now + TimeSpan.FromDays(90),
        //                        Customers = new SelectList(Repository.Instance.GetCustomers(), "Id", "Name")
        //                    };

        //    return View(model);
        //}

        [HttpPost]
        public ActionResult Search(SearchViewModel searchViewModel)
        {
            if (ModelState.IsValid)
            {
                var query = new EmployeeQuery
                                {
                                    BeginOfWorkPeriod = searchViewModel.StartDate,
                                    EndOfWorkPeriod = searchViewModel.EndDate,
                                    CustomerId = searchViewModel.SelectedCustomer
                                };
                var evaluationService = new EmployeeEvaluationService();
                var resultModel = evaluationService.FindEmployees(query)
                    .Select(e => new FoundEmployeeViewModel { Id = e.Id, Name = String.Format("{0} {1}", e.FirstName, e.LastName) });
                
                return View("SearchResult", resultModel);
            }

            searchViewModel.Customers = new SelectList(Repository.Instance.GetCustomers(), "Id", "Name", searchViewModel.SelectedCustomer);
            return View(searchViewModel);
        }
    }

}
