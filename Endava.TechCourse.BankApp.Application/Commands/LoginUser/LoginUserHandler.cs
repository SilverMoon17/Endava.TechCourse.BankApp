using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Endava.TechCourse.BankApp.Application.Commands.LoginUser
{
	public class LoginUserHandler : IRequestHandler<LoginUserCommand, CommandStatus>
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public LoginUserHandler(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			ArgumentNullException.ThrowIfNull(userManager);
			ArgumentNullException.ThrowIfNull(signInManager);

			this._userManager = userManager;
			this._signInManager = signInManager;
		}

		public async Task<CommandStatus> Handle(LoginUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByNameAsync(request.Username);

			if (user is null)
				return CommandStatus.Failed("Nu exista un asemenea utilizator");

			var passwordStatus = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (!passwordStatus.Succeeded)
				return CommandStatus.Failed("Parola introdusa este gresita");

			return new();
		}
	}
}