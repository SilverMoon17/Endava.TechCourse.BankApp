using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetCurrencies
{
	public class GetCurrenciesQuery : IRequest<List<Currency>>
	{
	}
}