using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.TransferFounds
{
	public class TransferFundsByWalletCodeCommand : IRequest<CommandStatus>

	{
		public string SenderWalletId { get; set; }
		public string WalletCode { get; set; }
		public decimal Amount { get; set; }
	}
}