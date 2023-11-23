using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using Endava.TechCourse.BankApp.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.CreateWallet
{
	public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _context;
		private readonly WalletCodeGenerator _walletCodeGenerator;

		public CreateWalletCommandHandler(ApplicationDbContext context, WalletCodeGenerator walletCodeGenerator)
		{
			ArgumentNullException.ThrowIfNull(context);
			ArgumentNullException.ThrowIfNull(walletCodeGenerator);

			_context = context;
			_walletCodeGenerator = walletCodeGenerator;
		}

		public async Task<CommandStatus> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
		{
			if (!_context.Users.Any(u => u.Id.ToString() == request.OwnerId)) return CommandStatus.Failed("User doesn't exists");

			var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyCode == request.CurrencyCode);

			if (currency is null)
				return CommandStatus.Failed("Currency doesn't exists");

			var walletType = await _context.WalletTypes.FirstOrDefaultAsync(wt => wt.WalletTypeName == request.WalletTypeName, cancellationToken);

			if (walletType is null)
				return CommandStatus.Failed("This wallet type doesn't exists!");

			if (await _context.Wallets.Where(w => w.OwnerId.ToString() == request.OwnerId).AnyAsync(w => w.IsMain == true, cancellationToken) && request.IsMain)
				return CommandStatus.Failed("You can have only 1 main wallet!");

			var walletCode = _walletCodeGenerator.GenerateWalletCode();

			var wallet = new Wallet
			{
				OwnerId = Guid.Parse(request.OwnerId),
				WalletName = request.WalletName,
				WalletCode = walletCode,
				Type = walletType,
				Amount = request.Amount,
				Currency = currency,
				IsMain = request.IsMain
			};

			await _context.Wallets.AddAsync(wallet, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return new();
		}
	}
}