using Endava.TechCourse.BankApp.Domain.Dtos;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetUserDetails
{
	public class GetUserDetailsQuery : IRequest<UserDetails>
	{
		public string Username { get; set; }
	}
}