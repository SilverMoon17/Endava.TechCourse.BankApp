using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.UpdateWalletIsFavorite
{
	public class UpdateWalletIsFavoriteCommand : IRequest<CommandStatus>
	{
		public string Id { get; set; }
		public bool IsFavorite { get; set; }
	}
}