using Endava.TechCourse.BankApp.Application.Commands;
using Endava.TechCourse.BankApp.Application.Commands.AddCurrency;
using Endava.TechCourse.BankApp.Server.Controllers;
using Endava.TechCourse.BankApp.Shared;
using Endava.TechCourse.BankApp.Test.Common;
using MediatR;
using NSubstitute;

namespace Endava.TechCourse.BankApp.Test.ControllersTests
{
	public class CurrencyControllerTests
	{
		[Test, ApplicationData]
		public async Task ShouldSaveCurrency
		(
			[Frozen] IMediator mediator,
			[Greedy] CurrencyController controller,
			CurrencyDto dto
		)
		{
			var expectedCommand = new AddCurrencyCommand()
			{
				Name = dto.Name,
				ChangeRate = dto.ChangeRate,
				CurrencyCode = dto.CurrencyCode,
			};

			mediator.Send(Arg.Any<AddCurrencyCommand>()).Returns(new CommandStatus());

			await controller.AddCurrency(dto);

			mediator.Received(1).Send(Arg.Is<AddCurrencyCommand>(x => x.Name == dto.Name &&
																	  x.ChangeRate == dto.ChangeRate &&
																	  x.CurrencyCode == dto.CurrencyCode));
		}

		[Test, ApplicationData]
		public async Task ShouldSaveCurrencyReturnOk
		(
			[Frozen] IMediator mediator,
			[Greedy] CurrencyController controller,
			CurrencyDto dto
		)
		{
			var expectedCommand = new AddCurrencyCommand()
			{
				Name = dto.Name,
				ChangeRate = dto.ChangeRate,
				CurrencyCode = dto.CurrencyCode,
			};

			mediator.Send(Arg.Any<AddCurrencyCommand>()).Returns(new CommandStatus());

			var result = await controller.AddCurrency(dto);

			result.Should().Be(true);
		}
	}
}