using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.CreateWallet
{
	public class CreateWalletCommand : IRequest
	{
		public string Type { get; set; }
		public decimal Amount { get; set; }
		public string CurrencyCode { get; set; }
	}
}