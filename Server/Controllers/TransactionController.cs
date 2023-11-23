using Endava.TechCourse.BankApp.Application.Commands;
using Endava.TechCourse.BankApp.Application.Commands.TransferFounds;
using Endava.TechCourse.BankApp.Application.Commands.TransferFundsByEmail;
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
		public async Task<IActionResult> TransferFunds([FromBody] SendTransactionDto dto)
		{
			var result = new CommandStatus() { IsSuccessful = false };
			if (dto.WalletCode != String.Empty)
			{
				var command = new TransferFundsByWalletCodeCommand()
				{
					Amount = dto.Amount,
					WalletCode = dto.WalletCode,
					SenderWalletId = dto.SenderWalletId,
				};
				result = await _mediator.Send(command);
			}
			else if (dto.ReceiverEmail != String.Empty)
			{
				var command = new TransferFundsByEmailCommand()
				{
					Amount = dto.Amount,
					Email = dto.ReceiverEmail,
					SenderWalletId = dto.SenderWalletId,
				};

				result = await _mediator.Send(command);
			}

			return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
		}

		[HttpGet]
		[Authorize]
		[Route("allReceivedTransactions")]
		public async Task<IEnumerable<TransactionDto>> GetAllReceivedTransfers()
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName)?.Value;

			if (userIdClaim is null)
				return new List<TransactionDto>();

			var query = new GetAllReceivedTransfersQuery()
			{
				ReceiverId = userIdClaim
			};

			var transactions = await _mediator.Send(query);

			return Mapper.Map(transactions);
		}

		[HttpGet]
		[Authorize]
		[Route("allSendTransactions")]
		public async Task<IEnumerable<TransactionDto>> GetAllSendTransfers()
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName)?.Value;

			if (userIdClaim is null)
				return new List<TransactionDto>();

			var query = new GetAllSendTransfersQuery()
			{
				SenderId = userIdClaim
			};

			var transactions = await _mediator.Send(query);

			return Mapper.Map(transactions);
		}
	}
}