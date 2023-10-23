using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
	public class Transaction : BaseEntity
	{
		public Guid SenderId { get; set; }
		public Guid ReceiverId { get; set; }
		public Currency Currency { get; set; }
		public decimal Amount { get; set; }
	}
}