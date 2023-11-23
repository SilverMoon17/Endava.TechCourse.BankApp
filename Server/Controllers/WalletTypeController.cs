using Endava.TechCourse.BankApp.Application.Commands.CreateWalletType;
using Endava.TechCourse.BankApp.Application.Commands.DeleteWalletType;
using Endava.TechCourse.BankApp.Application.Commands.UpdateWalletTypeById;
using Endava.TechCourse.BankApp.Application.Queries.GetAllWalletTypes;
using Endava.TechCourse.BankApp.Application.Queries.GetWalletTypeById;
using Endava.TechCourse.BankApp.Server.Common;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
	[Route("api/walletTypeController")]
	[ApiController]
	public class WalletTypeController : ControllerBase
	{
		private readonly IMediator _mediator;

		public WalletTypeController(IMediator mediator)
		{
			ArgumentNullException.ThrowIfNull(mediator);

			_mediator = mediator;
		}

		[HttpPost]
		[Route("createWalletType")]
		public async Task<IActionResult> CreateWalletType([FromBody] CreateWalletTypeDto createWalletTypeDto)
		{
			var command = new CreateWalletTypeCommand()
			{
				WalletTypeName = createWalletTypeDto.WalletTypeName,
				Commission = createWalletTypeDto.Commission
			};

			var result = await _mediator.Send(command);

			return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
		}

		[HttpGet]
		[Route("getAllWalletTypes")]
		public async Task<IActionResult> GetAllWalletTypes()
		{
			var query = new GetAllWalletTypesQuery();

			var walletTypesList = await _mediator.Send(query);

			return Ok(Mapper.Map(walletTypesList));
		}

		[HttpGet("getWalletTypeById/{id}")]
		public async Task<IActionResult> GetWalletTypeById(string id)
		{
			var query = new GetWalletTypeByIdQuery()
			{
				Id = id
			};

			var walletType = await _mediator.Send(query);

			return Ok(Mapper.Map(walletType));
		}

		[HttpPost]
		[Route("updateWalletType")]
		public async Task<IActionResult> UpdateWalletType([FromBody] UpdateWalletTypeByIdDto updateWalletTypeByIdDto)
		{
			var command = new UpdateWalletTypeByIdCommand()
			{
				Id = updateWalletTypeByIdDto.Id,
				Name = updateWalletTypeByIdDto.Name,
				Commission = updateWalletTypeByIdDto.Commission,
			};

			var result = await _mediator.Send(command);

			return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
		}

		[HttpDelete]
		[Route("deleteWalletType/{id}")]
		public async Task<IActionResult> DeleteWalletType(Guid id)
		{
			var command = new DeleteWalletTypeCommand()
			{
				Id = id
			};
			var result = await _mediator.Send(command);

			return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
		}
	}
}