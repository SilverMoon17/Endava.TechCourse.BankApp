using AutoFixture.Idioms;
using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using Endava.TechCourse.BankApp.Server.Controllers;
using Endava.TechCourse.BankApp.Test.Common;
using FluentAssertions;

namespace Endava.TechCourse.BankApp.Test.ControllersTests
{
	public class WalletControllerTests
	{
		[Test, ApplicationData]
		public void CanCreateInstance(GuardClauseAssertion assertion)
			=> assertion.Verify(typeof(WalletController).GetConstructors());

		[Test, ApplicationData]
		public async Task ShouldGetWallets(
			[Frozen] ApplicationDbContext context,
			[Greedy] WalletController controller,
			Wallet firstWallet,
			Wallet secondWallet)
		{
			//Arrange
			context.Wallets.AddRange(firstWallet, secondWallet);
			context.SaveChanges();
			context.ChangeTracker.Clear();

			//Act
			var result = await controller.GetWallets();

			//Assert
			result.Count.Should().Be(2);
		}
	}
}