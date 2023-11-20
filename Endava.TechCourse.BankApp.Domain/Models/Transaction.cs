using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
	public class Transaction : BaseEntity
	{
		public string SenderUsername { get; set; }
		public Guid SenderWalletId { get; set; }
		public string ReceiverUsername { get; set; }
		public Guid ReceiverWalletId { get; set; }
		public Currency SenderCurrency { get; set; }
		public Currency ReceiverCurrency { get; set; }
		public decimal AmountInReceiverCurrency { get; set; }
	}
}