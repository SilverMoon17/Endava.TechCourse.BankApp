using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetAllReceivedTransfers
{
	public class GetAllSendTransfersHandler : IRequestHandler<GetAllSendTransfersQuery, List<Transaction>>
	{
		private readonly ApplicationDbContext _context;

		public GetAllSendTransfersHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public async Task<List<Transaction>> Handle(GetAllSendTransfersQuery request, CancellationToken cancellationToken)
		{
			var senderWallet =
				await _context.Wallets.FirstOrDefaultAsync(w => w.OwnerId.ToString() == request.SenderId,
					cancellationToken);
			if (senderWallet == null)
			{
				throw new ArgumentNullException();
			}

			var transactions = await _context.Transactions.Where(t => t.SenderWalletId == senderWallet.Id)
				.Include(t => t.ReceiverCurrency)
				.Include(t => t.SenderCurrency)
				.ToListAsync(cancellationToken);

			return transactions;
		}
	}
}