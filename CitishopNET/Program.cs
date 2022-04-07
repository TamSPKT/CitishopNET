using CitishopNET.Business;
using CitishopNET.DataAccess;
using CitishopNET.DataAccess.Data;
using CitishopNET.DataAccess.Models;
using CitishopNET.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCitishopDataAccessLayer(builder.Configuration);
builder.Services.AddCitishopBusinessLayer();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
	options.SignIn.RequireConfirmedAccount = true;

	options.Password.RequireNonAlphanumeric = false;

	options.User.RequireUniqueEmail = true;
})
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews()
	.AddFluentValidation(fv =>
	{
		fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		fv.DisableDataAnnotationsValidation = true;
		ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
	});

builder.Services.AddSwaggerGen(options =>
{
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
else
{
	app.UseMigrationsEndPoint();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();