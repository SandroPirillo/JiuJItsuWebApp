using JiuJitsuWebApp.Data;
using JiuJitsuWebApp.Services;
using Microsoft.AspNetCore.Builder;

namespace JiuJitsuWebApp
{
	public class Startup
	{

		public void ConfigureServices(IServiceCollection services)
		{

			services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30); // You can set the timeout duration as per your need
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
		}
		public void Configure(IApplicationBuilder app)
		{
			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
			app.UseSession();
			app.UseAuthentication();
			app.UseUserEmailMiddleware();



		}
	}
}
