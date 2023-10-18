using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
	public class Currency : BaseEntity
	{
		public string Name { get; set; }
		public string CurrencyCode { get; set; }
		public string ChangeRate { get; set; }
	}
}