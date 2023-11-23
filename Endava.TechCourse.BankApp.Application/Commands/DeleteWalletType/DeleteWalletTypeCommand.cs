using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteWalletType
{
	public class DeleteWalletTypeCommand : IRequest<CommandStatus>
	{
		public Guid Id { get; set; }
	}
}