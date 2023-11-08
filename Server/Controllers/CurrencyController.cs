using Endava.TechCourse.BankApp.Application.Commands.AddCurrency;
using Endava.TechCourse.BankApp.Application.Commands.DeleteCurrency;
using Endava.TechCourse.BankApp.Application.Queries.GetCurrencies;
using Endava.TechCourse.BankApp.Application.Queries.GetCurrencyById;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
	[Route("api/currencies")]
	[ApiController]
	public class CurrencyController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CurrencyController(IMediator mediator)
		{
			ArgumentNullException.ThrowIfNull(mediator);
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> AddCurrency([FromBody] CurrencyDto currencyDTO)
		{
			var command = new AddCurrencyCommand()
			{
				Name = currencyDTO.Name,
				CurrencyCode = currencyDTO.CurrencyCode,
				ChangeRate = currencyDTO.ChangeRate,
			};

			var result = await _mediator.Send(command);

			return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
		}

		[HttpGet]
		public async Task<List<CurrencyDto>> GetCurrencies()
		{
			var query = new GetCurrenciesQuery();
			var currencies = await _mediator.Send(query);
			var dtos = new List<CurrencyDto>();

			foreach (var currency in currencies)
			{
				var dto = new CurrencyDto()
				{
					CanBeRemoved = true,
					ChangeRate = currency.ChangeRate,
					CurrencyCode = currency.CurrencyCode,
					Id = currency.Id.ToString(),
					Name = currency.Name,
				};

				dtos.Add(dto);
			}

			return dtos;
		}

		[HttpGet("{id}")]
		public async Task<CurrencyDto?> GetCurrencyById(Guid id)
		{
			var query = new GetCurrencyByIdQuery()
			{
				Id = id
			};

			var currency = await _mediator.Send(query);

			var dto = new CurrencyDto()
			{
				CanBeRemoved = true,
				ChangeRate = currency.ChangeRate,
				CurrencyCode = currency.CurrencyCode,
				Id = currency.Id.ToString(),
				Name = currency.Name
			};

			return dto;
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCurrency(Guid id)
		{
			var command = new DeleteCurrencyCommand()
			{
				Id = id
			};
			var result = await _mediator.Send(command);

			return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
		}
	}
}