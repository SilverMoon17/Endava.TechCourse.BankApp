using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetAllWalletTypes
{
	public class GetAllWalletTypesHandler : IRequestHandler<GetAllWalletTypesQuery, List<WalletType>>
	{
		private readonly ApplicationDbContext _context;

		public GetAllWalletTypesHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);

			_context = context;
		}

		public async Task<List<WalletType>> Handle(GetAllWalletTypesQuery request, CancellationToken cancellationToken)
		{
			return await _context.WalletTypes.AsNoTracking().ToListAsync(cancellationToken);
		}
	}
}