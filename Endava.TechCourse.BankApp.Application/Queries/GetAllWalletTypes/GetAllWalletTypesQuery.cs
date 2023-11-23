using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetAllWalletTypes
{
	public class GetAllWalletTypesQuery : IRequest<List<WalletType>>
	{
	}
}