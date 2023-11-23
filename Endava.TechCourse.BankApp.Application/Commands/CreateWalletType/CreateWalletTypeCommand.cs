using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.CreateWalletType
{
	public class CreateWalletTypeCommand : IRequest<CommandStatus>
	{
		public string WalletTypeName { get; set; }
		public decimal Commission { get; set; }
	}
}