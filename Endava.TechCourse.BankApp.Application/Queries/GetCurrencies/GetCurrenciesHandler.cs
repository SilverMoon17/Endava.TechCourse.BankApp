using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Queries.GetCurrencies
{
	public class GetCurrenciesHandler : IRequestHandler<GetCurrenciesQuery, List<Currency>>
	{
		private readonly ApplicationDbContext _context;

		public GetCurrenciesHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public async Task<List<Currency>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
		{
			return await _context.Currencies.AsNoTracking().ToListAsync();
		}
	}
}