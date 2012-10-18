using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zuehlke.Zmapp.WebApp.Models
{
	public class CustomerViewModel
	{
		[DisplayName("Customer Id:")]
		public int Id { get; set; }

		[DisplayName("Customer Name:")]
		[Required]
		[MaxLength(20, ErrorMessage = "Customer name must be less than 20 characters")]
		[MinLength(2, ErrorMessage = "Customer name must be at least 2 characters")]
		public string Name { get; set; }

		[DisplayName("City")]
		public string City { get; set; }
	}
}