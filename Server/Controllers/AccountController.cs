using Endava.TechCourse.BankApp.Application.Commands.RegisterUser;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AccountController(IMediator mediator)
		{
			ArgumentNullException.ThrowIfNull(mediator);
			_mediator = mediator;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto dto)
		{
			var command = new RegisterUserCommand()
			{
				Email = dto.Email,
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Password = dto.Password,
				Username = dto.Username
			};

			var result = await _mediator.Send(command);

			return result.IsSuccessful ? Ok(result) : BadRequest(new { result.Error });
		}
	}
}