using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetCurrencyById
{
	public class GetCurrencyByIdQuery : IRequest<Currency>
	{
		public Guid Id { get; set; }
	}
}