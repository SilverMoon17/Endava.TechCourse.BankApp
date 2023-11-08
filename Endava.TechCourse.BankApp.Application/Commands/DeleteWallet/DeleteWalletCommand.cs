using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteWallet
{
	public class DeleteWalletCommand : IRequest<CommandStatus>
	{
		public Guid Id { get; set; }
	}
}