using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Endava.TechCourse.BankApp.Infrastructure;

public static class ServicesExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(
				configuration.GetConnectionString("DefaultConnection"),
				b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

		services
			.AddIdentity<User, IdentityRole<Guid>>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddRoleStore<RoleStore<IdentityRole<Guid>, ApplicationDbContext, Guid>>()
			.AddUserStore<UserStore<User, IdentityRole<Guid>, ApplicationDbContext, Guid>>()
			.AddUserManager<UserManager<User>>()
			.AddRoleManager<RoleManager<IdentityRole<Guid>>>()
			.AddSignInManager<SignInManager<User>>()
			.AddDefaultTokenProviders();

		services.Configure<IdentityOptions>(options =>
		{
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireUppercase = false;
			options.Password.RequireDigit = false;
			options.Password.RequireLowercase = false;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequiredLength = 3;
			options.User.RequireUniqueEmail = false;
			options.Password.RequiredUniqueChars = 1;
			options.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		});

		return services;
	}
}