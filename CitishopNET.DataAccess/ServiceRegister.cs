using CitishopNET.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CitishopNET.DataAccess
{
	public static class ServiceRegister
	{
		public static void AddCitishopDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<ApplicationDbContext>(
				options => options.UseSqlServer(
					connectionString,
					b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
			services.AddDatabaseDeveloperPageExceptionFilter();

		}
	}
}