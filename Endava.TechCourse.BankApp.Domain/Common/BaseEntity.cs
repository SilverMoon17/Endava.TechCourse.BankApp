namespace Endava.TechCourse.BankApp.Domain.Common
{
	public class BaseEntity
	{
		public Guid Id { get; } = new Guid();
		public DateTime TimeStamp { get; } = DateTime.Now;
	}
}