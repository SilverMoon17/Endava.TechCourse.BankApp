using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
	public class WalletType : BaseEntity
	{
		public string WalletTypeName { get; set; }
		public decimal Commission { get; set; }
	}
}