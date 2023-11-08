using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetCurrencyById
{
	public class GetCurrencyByIdHandler : IRequestHandler<GetCurrencyByIdQuery, Currency>
	{
		private readonly ApplicationDbContext _context;

		public GetCurrencyByIdHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public async Task<Currency> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
		{
			return await _context.Currencies.FindAsync(request.Id);
		}
	}
}