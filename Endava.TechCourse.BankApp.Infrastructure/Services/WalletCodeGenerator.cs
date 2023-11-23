namespace Endava.TechCourse.BankApp.Infrastructure.Services
{
	public class WalletCodeGenerator
	{
		private Random random = new Random();

		public string GenerateWalletCode()
		{
			const int walletCodeLength = 16;
			const string firstDigitChars = "123456789"; // Exclude "0"
			const string otherDigitsChars = "0123456789";

			// Generate the first digit ensuring it is not "0"
			char firstDigit = firstDigitChars[random.Next(firstDigitChars.Length)];

			// Generate the remaining 15 digits
			string otherDigits = new string(Enumerable.Repeat(otherDigitsChars, walletCodeLength - 1)
				.Select(s => s[random.Next(s.Length)]).ToArray());

			// Concatenate the first and remaining digits
			string walletCode = firstDigit + otherDigits;

			return walletCode;
		}
	}
}