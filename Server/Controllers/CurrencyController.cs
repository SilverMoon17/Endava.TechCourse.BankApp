using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using Endava.TechCourse.BankApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CurrencyController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public CurrencyController(ApplicationDbContext dbContext)
		{
			_context = dbContext;
		}

		[HttpPost]
		public IActionResult AddNewCurrency([FromBody] AddCurrencyDTO currencyDTO)
		{
			var currency = new Currency
			{
				Name = currencyDTO.Name,
				CurrencyCode = currencyDTO.CurrencyCode,
				ChangeRate = currencyDTO.ChangeRate,
			};
			_context.Currencies.Add(currency);
			_context.SaveChanges();

			return Ok();
		}

		[HttpGet("getAllCurrencies")]
		public async Task<List<Currency>> GetWallets()
		{
			return await _context.Currencies.ToListAsync();
		}
	}
}