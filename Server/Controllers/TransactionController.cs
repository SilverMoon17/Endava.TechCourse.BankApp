using Endava.TechCourse.BankApp.Application.Commands.TransferFounds;
using Endava.TechCourse.BankApp.Application.Queries.GetAllReceivedTransfers;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using Endava.TechCourse.BankApp.Server.Common;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
	[Route("api/transaction")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TransactionController(ApplicationDbContext dbContext, IMediator mediator)
		{
			ArgumentNullException.ThrowIfNull(mediator);
			_mediator = mediator;
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> TransferFunds([FromBody] TransactionDto dto)
		{
			var command = new TransferFoundsCommand()
			{
				Amount = dto.Amount,
				ReceiverUsername = dto.ReceiverUsername,
				SenderWalletId = dto.SenderWalletId,
			};

			var result = await _mediator.Send(command);

			return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
		}

		[HttpGet]
		[Authorize]
		[Route("allReceivedTransactions")]
		public async Task<List<TransactionDto>> GetAllReceivedTransfers()
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName)?.Value;

			if (userIdClaim is null)
				return new List<TransactionDto>();

			var query = new GetAllReceivedTransfersQuery()
			{
				ReceiverId = userIdClaim
			};

			var transactions = await _mediator.Send(query);

			var dtos = new List<TransactionDto>();

			foreach (var transaction in transactions)
			{
				var dto = new TransactionDto()
				{
					SenderWalletId = transaction.SenderWalletId.ToString(),
					ReceiverUsername = transaction.ReceiverUsername,
					Amount = transaction.AmountInReceiverCurrency,
					SenderUsername = transaction.SenderUsername,
					ReceiverCurrencyCode = transaction.ReceiverCurrency.CurrencyCode,
					SenderCurrencyCode = transaction.SenderCurrency.CurrencyCode,
					Id = transaction.Id.ToString(),
				};

				dtos.Add(dto);
			}

			return dtos;
		}

		[HttpGet]
		[Authorize]
		[Route("allSendTransactions")]
		public async Task<List<TransactionDto>> GetAllSendTransfers()
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName)?.Value;

			if (userIdClaim is null)
				return new List<TransactionDto>();

			var query = new GetAllSendTransfersQuery()
			{
				SenderId = userIdClaim
			};

			var transactions = await _mediator.Send(query);

			var dtos = new List<TransactionDto>();

			foreach (var transaction in transactions)
			{
				var dto = new TransactionDto()
				{
					SenderWalletId = transaction.SenderWalletId.ToString(),
					ReceiverUsername = transaction.ReceiverUsername,
					Amount = transaction.AmountInReceiverCurrency,
					SenderUsername = transaction.SenderUsername,
					ReceiverCurrencyCode = transaction.ReceiverCurrency.CurrencyCode,
					SenderCurrencyCode = transaction.SenderCurrency.CurrencyCode,
					Id = transaction.Id.ToString(),
				};

				dtos.Add(dto);
			}

			return dtos;
		}
	}
}