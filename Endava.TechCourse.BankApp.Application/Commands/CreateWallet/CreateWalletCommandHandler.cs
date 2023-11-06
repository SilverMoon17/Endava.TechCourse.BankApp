using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.CreateWallet
{
	public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand>
	{
		private readonly ApplicationDbContext _context;

		public CreateWalletCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Handle(CreateWalletCommand request, CancellationToken cancellationToken)
		{
			var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyCode == request.CurrencyCode);
			if (currency == null) throw new Exception();
			var wallet = new Wallet
			{
				Type = request.Type,
				Amount = request.Amount,
				Currency = currency
			};

			await _context.Wallets.AddAsync(wallet, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}