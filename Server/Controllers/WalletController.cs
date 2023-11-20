using Endava.TechCourse.BankApp.Application.Commands.CreateWallet;
using Endava.TechCourse.BankApp.Application.Commands.DeleteWallet;
using Endava.TechCourse.BankApp.Application.Queries.GetWalletById;
using Endava.TechCourse.BankApp.Application.Queries.GetWallets;
using Endava.TechCourse.BankApp.Application.Queries.GetWalletsByUserId;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using Endava.TechCourse.BankApp.Server.Common;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalletController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IMediator _mediator;

		public WalletController(ApplicationDbContext dbContext, IMediator mediator)
		{
			ArgumentNullException.ThrowIfNull(dbContext);
			ArgumentNullException.ThrowIfNull(mediator);
			_context = dbContext;
			_mediator = mediator;
		}

		[HttpGet]
		[Route("getWallets")]
		[Authorize(Roles = "User,Admin")]
		public async Task<List<WalletDto>> GetWallets()
		{
			var query = new GetWalletsQuery();

			var wallets = await _mediator.Send(query);

			var dtos = new List<WalletDto>();

			foreach (var wallet in wallets)
			{
				var dto = new WalletDto()
				{
					OwnerId = wallet.OwnerId.ToString(),
					Id = wallet.Id.ToString(),
					Currency = wallet.Currency.CurrencyCode,
					Type = wallet.Type,
					Amount = wallet.Amount
				};

				dtos.Add(dto);
			}

			return dtos;
		}

		[HttpGet]
		[Route("getWalletsForUser")]
		[Authorize]
		public async Task<List<WalletDto>> GetWalletsForUser()
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName)?.Value;

			if (userIdClaim is null)
				return new List<WalletDto>();

			var query = new GetWalletsByUserIdQuery()
			{
				OwnerId = userIdClaim
			};

			var wallets = await _mediator.Send(query);

			var dtos = new List<WalletDto>();

			foreach (var wallet in wallets)
			{
				var dto = new WalletDto()
				{
					Id = wallet.Id.ToString(),
					Currency = wallet.Currency.CurrencyCode,
					Type = wallet.Type,
					Amount = wallet.Amount
				};

				dtos.Add(dto);
			}

			return dtos;
		}

		[HttpGet("{id}")]
		public async Task<WalletDto> GetWalletById(Guid id)
		{
			var query = new GetWalletByIdQuery()
			{
				Id = id
			};

			var wallet = await _mediator.Send(query);

			var dto = new WalletDto()
			{
				Amount = wallet.Amount,
				Currency = wallet.Currency.CurrencyCode,
				Type = wallet.Type,
				Id = wallet.Id.ToString()
			};

			return dto;
		}

		[HttpPost]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDTO createWalletDto)
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName)?.Value;

			var command = new CreateWalletCommand()
			{
				OwnerId = userIdClaim,
				Type = createWalletDto.Type,
				Amount = createWalletDto.Amount,
				CurrencyCode = createWalletDto.CurrencyCode,
			};

			var result = await _mediator.Send(command);

			return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteWallet(Guid id)
		{
			var command = new DeleteWalletCommand()
			{
				Id = id
			};
			var result = await _mediator.Send(command);

			return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
		}
	}
}