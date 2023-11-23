using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
	public class Transaction : BaseEntity
	{
		public string SenderUsername { get; set; }
		public string SenderEmail { get; set; }
		public string WalletCode { get; set; }
		public Guid ReceiverId { get; set; }
		public Guid SenderId { get; set; }
		public Guid SenderWalletId { get; set; }
		public string ReceiverUsername { get; set; }
		public Guid ReceiverWalletId { get; set; }
		public Currency SenderCurrency { get; set; }
		public Currency ReceiverCurrency { get; set; }
		public decimal AmountInReceiverCurrency { get; set; }

		public static Transaction From(User senderUser, User receiverUser, Wallet senderWallet, Wallet receiverWallet)
		{
			return new Transaction()
			{
				SenderUsername = senderUser.UserName,
				SenderEmail = senderUser.Email,
				WalletCode = receiverWallet.WalletCode,
				ReceiverId = receiverUser.Id,
				SenderId = senderUser.Id,
				SenderWalletId = senderWallet.Id,
				ReceiverUsername = receiverUser.UserName,
				ReceiverWalletId = receiverWallet.Id,
				SenderCurrency = senderWallet.Currency,
				ReceiverCurrency = receiverWallet.Currency,
			};
		}
	}
}