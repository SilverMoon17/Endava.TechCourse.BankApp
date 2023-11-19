using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.TransferFounds
{
	public class TransferFoundsCommand : IRequest<CommandStatus>

	{
		public string SenderWalletId { get; set; }
		public string ReceiverUsername { get; set; }
		public decimal Amount { get; set; }
	}
}