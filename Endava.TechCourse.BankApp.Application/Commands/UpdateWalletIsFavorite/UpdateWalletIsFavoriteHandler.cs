using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.UpdateWalletIsFavorite
{
	public class UpdateWalletIsFavoriteHandler : IRequestHandler<UpdateWalletIsFavoriteCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _context;

		public UpdateWalletIsFavoriteHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);

			_context = context;
		}

		public async Task<CommandStatus> Handle(UpdateWalletIsFavoriteCommand request, CancellationToken cancellationToken)
		{
			var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id.ToString() == request.Id, cancellationToken);

			if (wallet is null)
				return CommandStatus.Failed("Wallet not found!");

			wallet.IsFavorite = request.IsFavorite;

			await _context.SaveChangesAsync(cancellationToken);

			return new();
		}
	}
}