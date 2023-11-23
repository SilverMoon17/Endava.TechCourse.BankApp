using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.TransferFundsByEmail
{
	public class TransferFundsByEmailCommand : IRequest<CommandStatus>
	{
		public string SenderWalletId { get; set; }
		public string Email { get; set; }
		public decimal Amount { get; set; }
	}
}