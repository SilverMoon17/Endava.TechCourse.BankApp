using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
	public class Report : BaseEntity
	{
		public decimal TotalSpent { get; set; }
		public string Type { get; set; }
		public int NumberOfTransactions { get; set; }
	}
}