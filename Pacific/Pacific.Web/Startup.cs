using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lib.Net.Http.WebPush;

using Pacific.Core.Models.Subscriptions;
using Pacific.Core.Producers;
using Pacific.Core.Services.Subscriptions;
using Pacific.Web.Models.Handlers;
using Microsoft.Extensions.Hosting;
using Pacific.Core.Services;
using Pacific.Core.Services.Orm;
using LinqToDB.Data;
using Pacific.ORM;
using AutoMapper;
using Pacific.Web.Mappings;

namespace Pacific.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			DataConnection.DefaultSettings = new DbSettings();
			services.AddAutoMapper(typeof(Startup).Assembly);

			services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "PacificClient/dist";
			});

			
			services.AddCors(option => option.AddPolicy("AllowAll", p => p.AllowAnyHeader()
																		.AllowAnyOrigin()
																		.AllowAnyMethod()));

			services.AddTransient<IHandlerAsync, GenericHandler>();
			services.AddTransient<OrmService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
            app.UseSpaStaticFiles();

			//app.UseRouting();
			app.UseCors("AllowAll");

			//app.UseAuthorization();
			/*app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});*/

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "PacificClient";
			});
		}
	}
}
