using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.TransferFounds
{
	public class TransferFundsByWalletCodeHandler : IRequestHandler<TransferFundsByWalletCodeCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _dbContext;

		public TransferFundsByWalletCodeHandler(ApplicationDbContext dbContext)
		{
			ArgumentNullException.ThrowIfNull(dbContext);

			_dbContext = dbContext;
		}

		public async Task<CommandStatus> Handle(TransferFundsByWalletCodeCommand request, CancellationToken cancellationToken)
		{
			var receiverWallet = await _dbContext.Wallets.Include(w => w.Currency)
					.Include(w => w.Type)
					.FirstOrDefaultAsync(w => w.WalletCode == request.WalletCode, cancellationToken);

			if (receiverWallet is null)
				return CommandStatus.Failed("Wallet with this wallet code not found!");

			var receiverUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == receiverWallet.OwnerId, cancellationToken);

			if (receiverUser is null)
				return CommandStatus.Failed("User doesn't have this wallet");

			var senderWallet = await _dbContext.Wallets.Include(w => w.Currency)
					.Include(w => w.Type)
					.FirstOrDefaultAsync(w => w.Id.ToString() == request.SenderWalletId, cancellationToken);

			var senderUser = await _dbContext.Users
				.FirstOrDefaultAsync(u => u.Id == senderWallet.OwnerId, cancellationToken);

			var senderCurrency = senderWallet.Currency;
			var receiverCurrency = receiverWallet.Currency;

			if (senderWallet.Amount < request.Amount) return CommandStatus.Failed("There are insufficient funds in the account!");

			decimal amountInReceiverCurrency =
				((senderCurrency.ChangeRate / receiverCurrency.ChangeRate) * request.Amount) - (request.Amount * senderWallet.Type.Commission / 100);

			senderWallet.Amount -= request.Amount;
			receiverWallet.Amount += amountInReceiverCurrency;

			Transaction transaction = Transaction.From(senderUser, receiverUser, senderWallet, receiverWallet);

			transaction.AmountInReceiverCurrency = amountInReceiverCurrency;

			await _dbContext.Transactions.AddAsync(transaction, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return new CommandStatus();
		}
	}
}