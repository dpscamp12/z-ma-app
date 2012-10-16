using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zuehlke.Zmapp.Services;

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
            var employee = Repository.Instance.GetEmployee(id);
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Employee(Employee employee)
        {
            Repository.Instance.SetEmployee(employee);
            return Json(employee);
        }

        [HttpDelete]
        public HttpStatusCodeResult DeleteEmployee(int id)
        {
            bool success = Repository.Instance.RemoveEmployee(id);
            return new HttpStatusCodeResult(success ? 200 : 400);  // are these good status codes? 
        }

    }
}
