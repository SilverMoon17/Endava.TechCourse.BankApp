using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
	public class Wallet : BaseEntity
	{
		public Guid OwnerId { get; set; }
		public string WalletName { get; set; }
		public WalletType Type { get; set; }
		public string WalletCode { get; set; }
		public decimal Amount { get; set; }
		public Currency Currency { get; set; }

		public Guid CurrencyId { get; set; }

		public bool IsMain { get; set; } = false;
		public bool IsFavorite { get; set; } = false;
	}
}