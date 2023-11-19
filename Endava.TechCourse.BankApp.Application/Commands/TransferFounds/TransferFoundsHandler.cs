using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.TransferFounds
{
	public class TransferFoundsHandler : IRequestHandler<TransferFoundsCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _dbContext;

		public TransferFoundsHandler(ApplicationDbContext dbContext)
		{
			ArgumentNullException.ThrowIfNull(dbContext);
			_dbContext = dbContext;
		}

		public async Task<CommandStatus> Handle(TransferFoundsCommand request, CancellationToken cancellationToken)
		{
			var receiverUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.ReceiverUsername, cancellationToken);

			if (receiverUser == null)
			{
				return CommandStatus.Failed("User with this username doesn't exists");
			}

			var receiverWallet = await _dbContext.Wallets.Include(w => w.Currency).FirstOrDefaultAsync(w => w.OwnerId == receiverUser.Id, cancellationToken);

			if (receiverWallet == null)
			{
				return CommandStatus.Failed("User doesn't have wallet with this ID");
			}

			var senderWallet =
				await _dbContext.Wallets.Include(w => w.Currency).FirstOrDefaultAsync(w => w.Id.ToString() == request.SenderWalletId, cancellationToken);

			var senderUser = await
				_dbContext.Users.FirstOrDefaultAsync(u => u.Id == senderWallet.OwnerId, cancellationToken);

			Currency senderCurrency = senderWallet.Currency;
			Currency receiverCurrency = receiverWallet.Currency;

			if (senderWallet.Amount < request.Amount)
			{
				return CommandStatus.Failed("There are insufficient funds in the account!");
			}

			decimal amountInReceiverCurrency = (senderCurrency.ChangeRate / receiverCurrency.ChangeRate) * request.Amount;

			senderWallet.Amount -= request.Amount;
			receiverWallet.Amount += amountInReceiverCurrency;

			Transaction transaction = new Transaction()
			{
				AmountInReceiverCurrency = amountInReceiverCurrency,
				SenderCurrency = senderCurrency,
				ReceiverCurrency = receiverCurrency,
				ReceiverWalletId = receiverWallet.Id,
				SenderWalletId = Guid.Parse(request.SenderWalletId),
				ReceiverUsername = request.ReceiverUsername,
				SenderUsername = senderUser.UserName
			};

			_dbContext.UpdateRange(senderWallet, receiverWallet);

			await _dbContext.Transactions.AddAsync(transaction, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return new CommandStatus();
		}
	}
}