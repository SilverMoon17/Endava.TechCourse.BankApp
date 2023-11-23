namespace Endava.TechCourse.BankApp.Shared
{
	public class WalletDto
	{
		public string OwnerId { get; set; }
		public string Id { get; set; }
		public string WalletTypeName { get; set; }
		public string WalletName { get; set; }
		public string WalletCode { get; set; }
		public string Currency { get; set; }
		public decimal Amount { get; set; }
		public bool IsMain { get; set; }
		public bool IsFavorite { get; set; }
	}
}