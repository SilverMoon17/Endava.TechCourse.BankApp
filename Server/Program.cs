using Endava.TechCourse.BankApp.Application.Commands.CreateWallet;
using Endava.TechCourse.BankApp.Application.Queries.GetWallets;
using Endava.TechCourse.BankApp.Infrastructure;
using Endava.TechCourse.BankApp.Server.Composition;

namespace Endava.TechCourse.BankApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var configuration = builder.Configuration;

			// Add services to the container.
			builder.Services.AddInfrastructure(configuration);
			builder.Services.AddJwtIdentity(configuration);
			builder.Services.AddMediatR(config =>
			{
				config.RegisterServicesFromAssembly(typeof(Program).Assembly);
				config.RegisterServicesFromAssembly(typeof(GetWalletsQuery).Assembly);
				config.RegisterServicesFromAssembly(typeof(CreateWalletCommand).Assembly);
			});

			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthorization();

			app.MapRazorPages();
			app.MapControllers();
			app.MapFallbackToFile("index.html");

			app.Run();
		}
	}
}