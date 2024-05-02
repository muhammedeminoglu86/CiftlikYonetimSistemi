using CiftlikYonetimSistemi.DAL.Context;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System.Configuration;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CiftlikYonetimSistemi.Extension;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
	.SetBasePath(AppContext.BaseDirectory)
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();
var redisCacheOptions = configuration.GetSection("RedisCacheOptions").Get<RedisCacheOptions>();
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = redisCacheOptions.Configuration;
	options.InstanceName = redisCacheOptions.InstanceName;
});
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
	var configuration = ConfigurationOptions.Parse(redisCacheOptions.Configuration, true);
	return ConnectionMultiplexer.Connect(configuration);
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<CreateMD5Hash>();
builder.Services.AddScoped<ResolveUrlInLinkExtension>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});


var dalAssembly = Assembly.Load("CiftlikYonetimSistemi.DAL");
var domainAssembly = Assembly.Load("CiftlikYonetimSistemi.DOMAIN");
var repositoryTypes = dalAssembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository")).ToList();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
	.AddCookie(options =>
	{
		options.LoginPath = "/Login/Login";
		options.LogoutPath = "/Login/Logout";
		options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
	});

foreach (var repoType in repositoryTypes)
{
	var repoInterface = domainAssembly.GetTypes().FirstOrDefault(t => t.IsInterface && $"I{repoType.Name}" == t.Name);
	if (repoInterface != null)
	{
		builder.Services.AddScoped(repoInterface, repoType);
	}
}

var businessAssembly = Assembly.Load("CiftlikYonetimSistemi.Business");
var repositoryTypesx = businessAssembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service")).ToList();

foreach (var repoType in repositoryTypesx)
{
	var repoInterface = businessAssembly.GetTypes().FirstOrDefault(t => t.IsInterface && $"I{repoType.Name}" == t.Name);
	if (repoInterface != null)
	{
		builder.Services.AddScoped(repoInterface, repoType);
	}
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger"; // URL'nin 'http://<your-url>/swagger' olarak eriþilmesini saðlar.
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
