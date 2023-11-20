using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetWalletsByUserId
{
	public class GetWalletsByUserIdQuery : IRequest<List<Wallet>>
	{
		public string OwnerId { get; set; }
	}
}