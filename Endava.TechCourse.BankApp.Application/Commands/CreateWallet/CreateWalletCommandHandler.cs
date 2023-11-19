using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.CreateWallet
{
	public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _context;

		public CreateWalletCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<CommandStatus> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
		{
			if (!_context.Users.Any(u => u.Id.ToString() == request.OwnerId))
			{
				return CommandStatus.Failed("User doesn't exists");
			}

			var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyCode == request.CurrencyCode);
			if (currency == null)
			{
				return CommandStatus.Failed("Currency doesn't exists");
			};
			var wallet = new Wallet
			{
				OwnerId = Guid.Parse(request.OwnerId),
				Type = request.Type,
				Amount = request.Amount,
				Currency = currency
			};

			await _context.Wallets.AddAsync(wallet, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return new CommandStatus();
		}
	}
}