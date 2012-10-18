using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zuehlke.Zmapp.Services.Client;
using Zuehlke.Zmapp.Services.Contracts.Customers;
using Zuehlke.Zmapp.WebApp.Models;

namespace Zuehlke.Zmapp.WebApp.Controllers
{
	public class CustomerController : Controller
	{
		private readonly ICustomerService customerService;

		public CustomerController()
			: this(new CustomerServiceProxy())
		{
		}

		public CustomerController(ICustomerService customerService)
		{
			if (customerService == null) throw new ArgumentNullException("customerService");

			this.customerService = customerService;
		}

		[HttpGet]
		public ActionResult Index()
		{
			IEnumerable<CustomerInfo> customers =this.customerService.GetCustomers();
			IEnumerable<CustomerViewModel> model = customers.Select(c => new CustomerViewModel
																			 {
																				 Id = c.Id,
																				 Name = c.Name,
																				 City = c.City
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
				var customer = new CustomerInfo
					               {
						               Id = model.Id,
						               Name = model.Name,
						               City = model.City,
					               };

				this.customerService.SetCustomer(customer);
			}

			return RedirectToAction("Index", "Customer");
		}

		[HttpGet]
		public ActionResult Delete(int customerId)
		{
			this.customerService.RemoveCustomer(customerId);

			return RedirectToAction("Index", "Customer");
		}

		private int GetUniqueId()
		{
			IEnumerable<CustomerInfo> customers = this.customerService.GetCustomers();
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
