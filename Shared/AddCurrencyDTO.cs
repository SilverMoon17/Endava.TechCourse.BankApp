namespace Endava.TechCourse.BankApp.Shared
{
	public class AddCurrencyDTO
	{
		public string Name { get; set; }
		public string CurrencyCode { get; set; }
		public decimal ChangeRate { get; set; }
	}
}