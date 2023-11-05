using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetWallets
{
	public class GetWalletsHandler : IRequestHandler<GetWalletsQuery, List<Wallet>>
	{
		private readonly ApplicationDbContext _context;

		public GetWalletsHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public async Task<List<Wallet>> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
		{
			var wallets = await _context.Wallets.Include(w => w.Currency).AsNoTracking().ToListAsync();

			return wallets;
		}
	}
}