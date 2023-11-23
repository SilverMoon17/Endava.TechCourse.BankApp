using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.UpdateWalletTypeById
{
	public class UpdateWalletTypeByIdHandler : IRequestHandler<UpdateWalletTypeByIdCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _context;

		public UpdateWalletTypeByIdHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);

			_context = context;
		}

		public async Task<CommandStatus> Handle(UpdateWalletTypeByIdCommand request, CancellationToken cancellationToken)
		{
			var walletType = await _context.WalletTypes.FirstOrDefaultAsync(wt => wt.Id.ToString() == request.Id, cancellationToken);

			if (walletType is null)
				return CommandStatus.Failed("Wallet Type not found!");

			walletType.WalletTypeName = request.Name;
			walletType.Commission = request.Commission;

			await _context.SaveChangesAsync(cancellationToken);

			return new();
		}
	}
}