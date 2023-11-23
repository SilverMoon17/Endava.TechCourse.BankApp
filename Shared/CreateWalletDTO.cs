namespace Endava.TechCourse.BankApp.Shared
{
	public class CreateWalletDto
	{
		public string WalletTypeName { get; set; }
		public string WalletName { get; set; }
		public decimal Amount { get; set; }
		public string CurrencyCode { get; set; }
		public bool IsMain { get; set; }
	}
}