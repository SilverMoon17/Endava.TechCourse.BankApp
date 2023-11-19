namespace Endava.TechCourse.BankApp.Shared
{
	public class WalletDto
	{
		public string OwnerId { get; set; }
		public string Id { get; set; }
		public string Currency { get; set; }
		public string Type { get; set; }
		public decimal Amount { get; set; }
	}
}