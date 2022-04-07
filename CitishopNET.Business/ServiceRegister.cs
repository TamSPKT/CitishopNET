using CitishopNET.Business.Repository;
using CitishopNET.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CitishopNET.Business
{
	public static class ServiceRegister
	{
		public static void AddCitishopBusinessLayer(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
			services.AddTransient(typeof(IProductService), typeof(ProductService));
			services.AddTransient(typeof(IUserService), typeof(UserService));

		}
	}
}