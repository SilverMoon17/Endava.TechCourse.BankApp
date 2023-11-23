using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteWallet
{
	internal class DeleteWalletHandler : IRequestHandler<DeleteWalletCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _context;

		public DeleteWalletHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);

			_context = context;
		}

		public async Task<CommandStatus> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
		{
			Wallet wallet = await _context.Wallets.FirstAsync(w => w.Id == request.Id, default);

			if (wallet is null) return CommandStatus.Failed("Wallet not found!");

			_context.Wallets.Remove(wallet);

			await _context.SaveChangesAsync(cancellationToken);

			return new();
		}
	}
}