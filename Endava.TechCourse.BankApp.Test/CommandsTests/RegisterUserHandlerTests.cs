using Endava.TechCourse.BankApp.Application.Commands.RegisterUser;
using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using Endava.TechCourse.BankApp.Test.Common;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Endava.TechCourse.BankApp.Test.CommandsTests
{
	public class RegisterUserHandlerTests
	{
		[Test, ApplicationData]
		public async Task Handle_UserDoesNotExist_ReturnsSuccess
		([Frozen] ApplicationDbContext dbContext,
			[Greedy] RegisterUserHandler handler
		)
		{
			var userManagerMock = new Mock<UserManager<User>>(
				Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

			userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
				.ReturnsAsync(IdentityResult.Success);

			userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
				.ReturnsAsync(IdentityResult.Success);

			handler = new RegisterUserHandler(dbContext, userManagerMock.Object);

			var request = new RegisterUserCommand
			{
				Username = "newuser",
				FirstName = "John",
				LastName = "Doe",
				Email = "newuser@example.com",
				Password = "password"
			};

			// Act
			var result = await handler.Handle(request, CancellationToken.None);

			// Assert
			Assert.That(result.IsSuccessful, Is.EqualTo(true));
		}

		[Test, ApplicationData]
		public async Task Handle_UserAlreadyExists_ReturnsFailure([Frozen] ApplicationDbContext dbContext,
			[Greedy] RegisterUserHandler handler)
		{
			// Arrange
			dbContext.Users.Add(new User
			{
				Email = "existinguser@example.com",
			});
			dbContext.SaveChanges();

			var userManagerMock = new Mock<UserManager<User>>(
				Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

			handler = new RegisterUserHandler(dbContext, userManagerMock.Object);

			var request = new RegisterUserCommand
			{
				Username = "existinguser",
				FirstName = "Jane",
				LastName = "Doe",
				Email = "existinguser@example.com",
				Password = "password"
			};

			// Act
			var result = await handler.Handle(request, CancellationToken.None);

			// Assert
			Assert.That(result.Error, Is.EqualTo("Utilizatorul deja exista"));
		}
	}
}