using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetWalletTypeById
{
	internal class GetWalletTypeByIdHandler : IRequestHandler<GetWalletTypeByIdQuery, WalletType>
	{
		private readonly ApplicationDbContext _context;

		public GetWalletTypeByIdHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public async Task<WalletType> Handle(GetWalletTypeByIdQuery request, CancellationToken cancellationToken)
		{
			return await _context.WalletTypes.FirstOrDefaultAsync(wt => wt.Id.ToString() == request.Id, cancellationToken);
		}
	}
}