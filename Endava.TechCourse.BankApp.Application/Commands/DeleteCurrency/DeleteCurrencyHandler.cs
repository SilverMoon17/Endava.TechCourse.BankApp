using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteCurrency
{
	public class DeleteCurrencyHandler : IRequestHandler<DeleteCurrencyCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _context;

		public DeleteCurrencyHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);
			_context = context;
		}

		public async Task<CommandStatus> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
		{
			Currency currency = await _context.Currencies.FirstAsync(c => c.Id == request.Id, default);

			if (currency == null)
			{
				return CommandStatus.Failed("Currency not found!");
			}

			_context.Currencies.Remove(currency);
			await _context.SaveChangesAsync(cancellationToken);

			return new CommandStatus();
		}
	}
}