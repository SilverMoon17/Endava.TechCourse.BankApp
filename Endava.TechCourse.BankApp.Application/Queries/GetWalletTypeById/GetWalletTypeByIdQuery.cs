using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetWalletTypeById
{
	public class GetWalletTypeByIdQuery : IRequest<WalletType>
	{
		public string Id { get; set; }
	}
}