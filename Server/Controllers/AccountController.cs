using Endava.TechCourse.BankApp.Application.Commands.LoginUser;
using Endava.TechCourse.BankApp.Application.Commands.RegisterUser;
using Endava.TechCourse.BankApp.Application.Queries.GetUserDetails;
using Endava.TechCourse.BankApp.Server.Common;
using Endava.TechCourse.BankApp.Server.Common.JWT;
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
		private readonly JwtService _jwtService;

		public AccountController(IMediator mediator, JwtService jwtService)
		{
			ArgumentNullException.ThrowIfNull(mediator);
			ArgumentNullException.ThrowIfNull(jwtService);
			_mediator = mediator;
			_jwtService = jwtService;
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

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			var loginCommand = new LoginUserCommand()
			{
				Username = dto.Username,
				Password = dto.Password
			};

			var result = await _mediator.Send(loginCommand);

			if (!result.IsSuccessful)
				return BadRequest(result.Error);

			var userDetailsQuery = new GetUserDetailsQuery()
			{
				Username = dto.Username
			};

			var userDetails = await _mediator.Send(userDetailsQuery);

			var jwtToken = _jwtService.CreateAuthToken(userDetails.Id, userDetails.Username, userDetails.Roles);

			Response.Cookies.Append(Constants.TokenCookieName, jwtToken, new CookieOptions()
			{
				HttpOnly = true,
				Expires = DateTimeOffset.MaxValue
			});

			return Ok(jwtToken);
		}
	}
}