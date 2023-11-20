using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetAllReceivedTransfers
{
	public class GetAllReceivedTransfersHandler : IRequestHandler<GetAllReceivedTransfersQuery, List<Transaction>>
	{
		private readonly ApplicationDbContext _context;

		public GetAllReceivedTransfersHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public async Task<List<Transaction>> Handle(GetAllReceivedTransfersQuery request, CancellationToken cancellationToken)
		{
			var receiverWallet =
				await _context.Wallets.FirstOrDefaultAsync(w => w.OwnerId.ToString() == request.ReceiverId,
					cancellationToken);
			if (receiverWallet == null)
			{
				throw new ArgumentNullException();
			}

			var transactions = await _context.Transactions
				.Where(t => t.ReceiverWalletId == receiverWallet.Id)
				.Include(t => t.ReceiverCurrency)
				.Include(t => t.SenderCurrency)
				.ToListAsync(cancellationToken);

			return transactions;
		}
	}
}