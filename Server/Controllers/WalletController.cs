using Endava.TechCourse.BankApp.Application.Commands.CreateWallet;
using Endava.TechCourse.BankApp.Application.Commands.DeleteWallet;
using Endava.TechCourse.BankApp.Application.Commands.UpdateWalletIsFavorite;
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
		private readonly IMediator _mediator;

		public WalletController(ApplicationDbContext dbContext, IMediator mediator)
		{
			ArgumentNullException.ThrowIfNull(mediator);

			_mediator = mediator;
		}

		[HttpGet]
		[Route("getWallets")]
		[Authorize(Roles = "User,Admin")]
		public async Task<IEnumerable<WalletDto>> GetWallets()
		{
			var query = new GetWalletsQuery();

			var wallets = await _mediator.Send(query);

			return Mapper.Map(wallets);
		}

		[HttpGet]
		[Route("getWalletsForUser")]
		[Authorize]
		public async Task<IEnumerable<WalletDto>> GetWalletsForUser()
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName)?.Value;

			if (userIdClaim is null)
				return new List<WalletDto>();

			var query = new GetWalletsByUserIdQuery()
			{
				OwnerId = userIdClaim
			};

			var wallets = await _mediator.Send(query);

			return Mapper.Map(wallets);
		}

		[HttpGet("{id}")]
		public async Task<WalletDto> GetWalletById(Guid id)
		{
			var query = new GetWalletByIdQuery()
			{
				Id = id
			};

			var wallet = await _mediator.Send(query);

			return Mapper.Map(wallet);
		}

		[HttpPost]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDto createWalletDto)
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName)?.Value;

			var command = new CreateWalletCommand()
			{
				OwnerId = userIdClaim,
				WalletName = createWalletDto.WalletName,
				WalletTypeName = createWalletDto.WalletTypeName,
				Amount = createWalletDto.Amount,
				CurrencyCode = createWalletDto.CurrencyCode,
				IsMain = createWalletDto.IsMain,
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

		[HttpPost]
		[Route("updateWalletIsFavorite")]
		public async Task<IActionResult> UpdateWalletIsFavorite([FromBody] UpdateWalletIsFavoriteDto updateWalletIsFavoriteDto)
		{
			var command = new UpdateWalletIsFavoriteCommand()
			{
				Id = updateWalletIsFavoriteDto.Id,
				IsFavorite = updateWalletIsFavoriteDto.IsFavorite
			};

			var result = await _mediator.Send(command);

			return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
		}
	}
}