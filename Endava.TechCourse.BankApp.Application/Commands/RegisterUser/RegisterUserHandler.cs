using Endava.TechCourse.BankApp.Domain.Enum;
using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.RegisterUser
{
	public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, CommandStatus>
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;

		public RegisterUserHandler(ApplicationDbContext context, UserManager<User> userManager)
		{
			ArgumentNullException.ThrowIfNull(context);
			ArgumentNullException.ThrowIfNull(userManager);

			this._context = context;
			this._userManager = userManager;
		}

		public async Task<CommandStatus> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var anyUser = await _context.Users.AnyAsync(cancellationToken);
			var userExists = await _context.Users.AnyAsync(user => user.Email == request.Email, cancellationToken);

			if (userExists)
				return CommandStatus.Failed("Utilizatorul deja exista");

			var user = new User()
			{
				Id = Guid.NewGuid(),
				UserName = request.Username,
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email
			};

			var createResult = await _userManager.CreateAsync(user, request.Password);

			IdentityResult roleResult;

			if (anyUser)
				roleResult = await _userManager.AddToRoleAsync(user, UserRole.User.ToString());
			else
				roleResult = await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

			if (!roleResult.Succeeded || !createResult.Succeeded)
				return CommandStatus.Failed("Utilizatorul nu a putut fi inregistrat");

			return new CommandStatus();
		}
	}
}