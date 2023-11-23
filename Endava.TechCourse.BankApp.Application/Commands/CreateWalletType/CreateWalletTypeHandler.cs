using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.CreateWalletType
{
	public class CreateWalletTypeHandler : IRequestHandler<CreateWalletTypeCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _context;

		public CreateWalletTypeHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);

			_context = context;
		}

		public async Task<CommandStatus> Handle(CreateWalletTypeCommand request, CancellationToken cancellationToken)
		{
			WalletType walletType = new WalletType()
			{
				WalletTypeName = request.WalletTypeName,
				Commission = request.Commission
			};

			await _context.AddAsync(walletType, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return new();
		}
	}
}