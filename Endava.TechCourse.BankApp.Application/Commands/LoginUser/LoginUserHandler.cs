using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Endava.TechCourse.BankApp.Application.Commands.LoginUser
{
	public class LoginUserHandler : IRequestHandler<LoginUserCommand, CommandStatus>
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;

		public LoginUserHandler(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			ArgumentNullException.ThrowIfNull(userManager);
			ArgumentNullException.ThrowIfNull(signInManager);

			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public async Task<CommandStatus> Handle(LoginUserCommand request, CancellationToken cancellationToken)
		{
			var user = await userManager.FindByNameAsync(request.Username);

			if (user is null)
				return CommandStatus.Failed("Nu exista un asemenea utilizator");

			var passwordStatus = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (!passwordStatus.Succeeded)
				return CommandStatus.Failed("Parola introdusa este gresita");

			return new();
		}
	}
}