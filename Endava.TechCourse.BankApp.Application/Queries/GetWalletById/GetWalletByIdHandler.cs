using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetWalletById
{
	public class GetWalletByIdHandler : IRequestHandler<GetWalletByIdQuery, Wallet>
	{
		private readonly ApplicationDbContext _context;

		public GetWalletByIdHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);

			_context = context;
		}

		public async Task<Wallet> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
		{
			return await _context.Wallets.Include(w => w.Currency).FirstAsync(w => request.Id == w.Id, cancellationToken);
		}
	}
}