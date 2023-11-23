using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.AddCurrency
{
	public class AddCurrencyHandler : IRequestHandler<AddCurrencyCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _context;

		public AddCurrencyHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);

			_context = context;
		}

		public async Task<CommandStatus> Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
		{
			if (await _context.Currencies.AnyAsync(x => x.Name == request.Name, default))
			{
				return CommandStatus.Failed("Currency with this name already exists");
			}

			if (await _context.Currencies.AnyAsync(x => x.CurrencyCode == request.CurrencyCode, default))
			{
				return CommandStatus.Failed("Currency with this currency code already exists");
			}

			var currency = new Currency()
			{
				Name = request.Name,
				CurrencyCode = request.CurrencyCode,
				ChangeRate = request.ChangeRate
			};

			await _context.Currencies.AddAsync(currency, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return new();
		}
	}
}