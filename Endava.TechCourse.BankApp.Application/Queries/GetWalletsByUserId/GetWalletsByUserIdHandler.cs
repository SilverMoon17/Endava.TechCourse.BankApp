using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetWalletsByUserId
{
	public class GetWalletsByUserIdHandler : IRequestHandler<GetWalletsByUserIdQuery, List<Wallet>>
	{
		private readonly ApplicationDbContext _context;

		public GetWalletsByUserIdHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public async Task<List<Wallet>> Handle(GetWalletsByUserIdQuery request, CancellationToken cancellationToken)
		{
			var wallets = await _context.Wallets.Where(w => w.OwnerId.ToString() == request.OwnerId).Include(w => w.Currency).AsNoTracking().ToListAsync(cancellationToken);

			return wallets;
		}
	}
}