using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteCurrency
{
	public class DeleteCurrencyCommand : IRequest<CommandStatus>
	{
		public Guid Id { get; set; }
	}
}