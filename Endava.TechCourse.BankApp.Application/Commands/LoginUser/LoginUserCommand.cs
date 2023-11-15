using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.LoginUser
{
	public class LoginUserCommand : IRequest<CommandStatus>
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}