using Endava.TechCourse.BankApp.Application.Commands.CreateWallet;
using Endava.TechCourse.BankApp.Application.Queries.GetWallets;
using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public async Task<List<WalletDto>> GetWallets()
		{
			var query = new GetWalletsQuery();

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
		public async Task<Wallet?> GetWalletById(Guid id)
		{
			return await _context.Wallets.Include(w => w.Currency).FirstOrDefaultAsync(w => w.Id == id);
		}

		[HttpPost]
		public IActionResult CreateWallet([FromBody] CreateWalletDTO createWalletDto)
		{
			var command = new CreateWalletCommand()
			{
				Type = createWalletDto.Type,
				Amount = createWalletDto.Amount,
				CurrencyCode = createWalletDto.CurrencyCode,
			};

			_mediator.Send(command);

			return Ok();
		}
	}
}