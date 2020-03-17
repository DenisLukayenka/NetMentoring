using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pacific.Core.Services;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Pacific.Web.Models.Handlers;
using Pacific.Web.Models.Requests;
using Pacific.Web.Models.Responses;
using Pacific.Web.Models;
using Pacific.Core.Models.Subscriptions;

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
			services.AddControllers();
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "PacificClient/dist";
			});

			services.AddTransient<IHandler, GenericHandler>();
			services.AddCors(option => option.AddPolicy("AllowAll", p => p.AllowAnyHeader()
																		.AllowAnyOrigin()
																		.AllowAnyMethod()));

			services.Configure<PushNotificationsOptions>(Configuration.GetSection("PushNotifications"));
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
			if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

			app.UseRouting();
			app.UseCors("AllowAll");

			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "PacificClient";
			});
		}
	}
}
