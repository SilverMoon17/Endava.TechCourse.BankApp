namespace Endava.TechCourse.BankApp.Shared
{
	public class SendTransactionDto
	{
		public string SenderWalletId { get; set; }
		public string ReceiverEmail { get; set; }
		public string WalletCode { get; set; }
		public decimal Amount { get; set; }
	}
}