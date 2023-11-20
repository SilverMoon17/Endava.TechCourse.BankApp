namespace Endava.TechCourse.BankApp.Shared
{
	public class TransactionDto
	{
		public string Id { get; set; }
		public string SenderUsername { get; set; }
		public string SenderWalletId { get; set; }
		public string SenderCurrencyCode { get; set; }
		public string ReceiverUsername { get; set; }
		public string ReceiverWalletId { get; set; }
		public string ReceiverCurrencyCode { get; set; }
		public decimal Amount { get; set; }
	}
}