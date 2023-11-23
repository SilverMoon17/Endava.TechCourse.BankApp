using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Shared;

namespace Endava.TechCourse.BankApp.Server.Common
{
	public static class Mapper
	{
		public static IEnumerable<CurrencyDto> Map(IEnumerable<Currency> currencies)
		{
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

		public static CurrencyDto Map(Currency currency)
		{
			return new CurrencyDto()
			{
				CanBeRemoved = true,
				ChangeRate = currency.ChangeRate,
				CurrencyCode = currency.CurrencyCode,
				Id = currency.Id.ToString(),
				Name = currency.Name,
			};
		}

		public static IEnumerable<WalletDto> Map(IEnumerable<Wallet> wallets)
		{
			var dtos = new List<WalletDto>();

			foreach (var wallet in wallets)
			{
				var dto = new WalletDto()
				{
					OwnerId = wallet.OwnerId.ToString(),
					Id = wallet.Id.ToString(),
					WalletCode = wallet.WalletCode,
					Currency = wallet.Currency.CurrencyCode,
					WalletTypeName = wallet.Type.WalletTypeName,
					WalletName = wallet.WalletName,
					Amount = wallet.Amount,
					IsMain = wallet.IsMain,
					IsFavorite = wallet.IsFavorite
				};

				dtos.Add(dto);
			}

			return dtos;
		}

		public static WalletDto Map(Wallet wallet)
		{
			return new WalletDto()
			{
				OwnerId = wallet.OwnerId.ToString(),
				Id = wallet.Id.ToString(),
				WalletCode = wallet.WalletCode,
				Currency = wallet.Currency.CurrencyCode,
				WalletTypeName = wallet.Type.WalletTypeName,
				WalletName = wallet.WalletName,
				Amount = wallet.Amount,
				IsMain = wallet.IsMain,
				IsFavorite = wallet.IsFavorite
			};
		}

		public static IEnumerable<TransactionDto> Map(IEnumerable<Transaction> transactions)
		{
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
					ReceiverWalletId = transaction.ReceiverWalletId.ToString(),
					SenderCurrencyCode = transaction.SenderCurrency.CurrencyCode,
					Id = transaction.Id.ToString(),
				};

				dtos.Add(dto);
			}

			return dtos;
		}

		public static TransactionDto Map(Transaction transaction)
		{
			return new TransactionDto()
			{
				SenderWalletId = transaction.SenderWalletId.ToString(),
				ReceiverUsername = transaction.ReceiverUsername,
				Amount = transaction.AmountInReceiverCurrency,
				SenderUsername = transaction.SenderUsername,
				ReceiverCurrencyCode = transaction.ReceiverCurrency.CurrencyCode,
				ReceiverWalletId = transaction.ReceiverWalletId.ToString(),
				SenderCurrencyCode = transaction.SenderCurrency.CurrencyCode,
				Id = transaction.Id.ToString(),
			};
		}

		public static IEnumerable<WalletTypeDto> Map(IEnumerable<WalletType> walletTypes)
		{
			var dtos = new List<WalletTypeDto>();

			foreach (var walletType in walletTypes)
			{
				var dto = new WalletTypeDto()
				{
					Id = walletType.Id.ToString(),
					WalletTypeName = walletType.WalletTypeName,
					Commission = walletType.Commission,
				};

				dtos.Add(dto);
			}

			return dtos;
		}

		public static WalletTypeDto Map(WalletType walletType)
		{
			return new WalletTypeDto()
			{
				Id = walletType.Id.ToString(),
				WalletTypeName = walletType.WalletTypeName,
				Commission = walletType.Commission,
			};
		}
	}
}