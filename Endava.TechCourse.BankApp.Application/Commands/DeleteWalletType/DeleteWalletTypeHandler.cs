using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteWalletType
{
	public class DeleteWalletTypeHandler : IRequestHandler<DeleteWalletTypeCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _context;

		public DeleteWalletTypeHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);

			_context = context;
		}

		public async Task<CommandStatus> Handle(DeleteWalletTypeCommand request, CancellationToken cancellationToken)
		{
			var walletType = await _context.WalletTypes.FirstOrDefaultAsync(wt => wt.Id == request.Id, cancellationToken);

			if (walletType is null) return CommandStatus.Failed("Wallet type not found!");

			_context.WalletTypes.Remove(walletType);

			await _context.SaveChangesAsync(cancellationToken);

			return new();
		}
	}
}