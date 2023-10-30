﻿using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using Endava.TechCourse.BankApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalletController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public WalletController(ApplicationDbContext dbContext)
		{
			_context = dbContext;
		}

		[HttpGet]
		[Route("getWallets")]
		public async Task<List<Wallet>> GetWallets()
		{
			return await _context.Wallets.Include(w => w.Currency).ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<Wallet?> GetWalletById(Guid id)
		{
			return await _context.Wallets.Include(w => w.Currency).FirstOrDefaultAsync(w => w.Id == id);
		}

		[HttpPost]
		public IActionResult CreateWallet([FromBody] CreateWalletDTO createWalletDto)
		{
			var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyCode == createWalletDto.CurrencyCode);
			var wallet = new Wallet
			{
				Type = createWalletDto.Type,
				Amount = createWalletDto.Amount,
				Currency = currency
			};

			_context.Wallets.Add(wallet);
			_context.SaveChanges();

			return Ok();
		}
	}
}