using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetAllReceivedTransfers
{
	public class GetAllSendTransfersQuery : IRequest<List<Transaction>>
	{
		public string SenderId { get; set; }
	}
}