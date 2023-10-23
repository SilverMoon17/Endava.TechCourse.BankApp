using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
	public class CashByCode : BaseEntity
	{
		public string Code { get; set; }
		public decimal Amount { get; set; }
		public Currency Currency { get; set; }
	}
}