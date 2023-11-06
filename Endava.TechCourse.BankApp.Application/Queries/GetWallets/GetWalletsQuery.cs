using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetWallets
{
	public class GetWalletsQuery : IRequest<List<Wallet>>
	{
	}
}