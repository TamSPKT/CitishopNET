﻿using CitishopNET.Business.Options;
using CitishopNET.Business.Repository;
using CitishopNET.Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CitishopNET.Business
{
	public static class ServiceRegister
	{
		public static void AddCitishopBusinessLayer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
			services.AddTransient(typeof(IProductService), typeof(ProductService));
			services.AddTransient(typeof(IUserService), typeof(UserService));

			services.AddTransient<IEmailSender, EmailSender>();
			services.Configure<AuthMessageSenderOptions>(configuration.GetSection(AuthMessageSenderOptions.AuthMessageSender));
		}
	}
}