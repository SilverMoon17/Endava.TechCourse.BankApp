using Endava.TechCourse.BankApp.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Endava.TechCourse.BankApp.Infrastructure.Configurations
{
	public class RoleConfigurations : IEntityTypeConfiguration<IdentityRole<Guid>>
	{
		public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
		{
			var roles = Enum.GetNames(typeof(UserRole))
				.Select(role => new IdentityRole<Guid>() { Id = Guid.NewGuid(), Name = role, NormalizedName = role.ToUpper() })
				.ToList();

			builder.HasData(roles);
		}
	}
}