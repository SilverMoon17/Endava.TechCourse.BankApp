using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.UpdateWalletTypeById
{
	public class UpdateWalletTypeByIdCommand : IRequest<CommandStatus>
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public decimal Commission { get; set; }
	}
}