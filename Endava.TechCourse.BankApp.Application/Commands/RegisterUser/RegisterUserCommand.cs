using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.RegisterUser
{
	public class RegisterUserCommand : IRequest<CommandStatus>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}