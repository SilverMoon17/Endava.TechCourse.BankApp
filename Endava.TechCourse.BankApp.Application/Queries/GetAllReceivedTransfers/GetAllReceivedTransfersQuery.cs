using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetAllReceivedTransfers
{
	public class GetAllReceivedTransfersQuery : IRequest<List<Transaction>>
	{
		public string ReceiverId { get; set; }
	}
}