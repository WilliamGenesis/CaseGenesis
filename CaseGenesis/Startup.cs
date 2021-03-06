﻿using BusinessLayer;
using CaseGenesis.Middleware;
using DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaseGenesis
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddDbContext<CaseContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:EmployeeDB"]));

			ConfigureContacts(services);
			ConfigureCompanies(services);
			ConfigureAddress(services);

			services.AddSingleton<IFakeObjectGenerator, FakeDataGenerator>();
		}

		private void ConfigureContacts(IServiceCollection services)
		{
			services.AddTransient<IContactRepository, ContactRepository>();
			services.AddTransient<IContactService, ContactService>();
		}

		private void ConfigureCompanies(IServiceCollection services)
		{
			services.AddTransient<ICompanyRepository, CompanyRepository>();
			services.AddTransient<ICompanyService, CompanyService>();
		}

		private void ConfigureAddress(IServiceCollection services)
		{
			services.AddTransient<IAddressRepository, AddressRepository>();
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseErrorHandlingMiddleware();
			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
