using System.Diagnostics.CodeAnalysis;
using CustomerManagement.Library.Repositories;
using CustomerManagement.Library.UseCases;
using CustomerManagement.Library.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudCustomerManagement.Api
{
    [ExcludeFromCodeCoverage]
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton(typeof(IAddCustomer), typeof(AddCustomer));
            services.AddSingleton(typeof(IUpdateCustomer), typeof(UpdateCustomer));
            services.AddSingleton(typeof(IGetCustomer), typeof(GetCustomer));
            services.AddSingleton(typeof(IDeleteCustomer), typeof(DeleteCustomer));
            services.AddSingleton(typeof(ICustomerRepository), typeof(CustomerDatastoreRepository));            
            services.AddSingleton(typeof(IRepositoryConfiguration), typeof(RepositoryConfiguration));
            services.AddSingleton(typeof(IIdentityResolver), typeof(IdentityResolver));
            services.AddSingleton(typeof(IDatastoreManager), typeof(DatastoreManager));

            services.AddSwaggerDocument();

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMvc();
        }
    }
}
