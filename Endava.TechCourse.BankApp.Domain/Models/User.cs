using Microsoft.AspNetCore.Identity;

namespace Endava.TechCourse.BankApp.Domain.Models
{
	public class User : IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}