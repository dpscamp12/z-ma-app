using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zuehlke.Zmapp.Services;
using Zuehlke.Zmapp.WebApp.Models;

namespace Zuehlke.Zmapp.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Customer> customers = Repository.Instance.GetCustomers();
            IEnumerable<CustomerViewModel> model = customers.Select(c => new CustomerViewModel
                                                                             {
                                                                                 Id = c.Id,
                                                                                 Name = c.Name,
                                                                                 City = "Zürich"
                                                                             });
            return View(model);
        }

        [HttpGet]
        public ActionResult New()
        {
            var model = new CustomerViewModel { Id = -1, Name = "", City = "" };
            return View(model);
        }

        [HttpPost]
        public ActionResult New(CustomerViewModel model, string submit)
        {
            model.Id = GetUniqueId();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (String.Equals(submit, "Save", StringComparison.InvariantCultureIgnoreCase))
            {
                Repository.Instance.SetCustomer(new Customer {Id = model.Id, Name = model.Name});
            }

            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        public ActionResult Delete(int customerId)
        {
            Repository.Instance.RemoveCustomer(customerId);
            return RedirectToAction("Index", "Customer");
        }

        private int GetUniqueId()
        {
            IEnumerable<Customer> customers = Repository.Instance.GetCustomers();
            if (customers == null)
            {
                return 1;
            }

            for (int newId = 1; newId < 10000; newId++)
            {
                if (!customers.Any(c => c.Id == newId))
                {
                    return newId;
                }
            }

            throw new InvalidOperationException("A maximum of 10000 customers is supported.");
        }
    }
}
