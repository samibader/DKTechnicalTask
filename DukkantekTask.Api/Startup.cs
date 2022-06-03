using DukkantekTask.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Globalization;
using System.Threading;

namespace DukkantekTask.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            //Setting Culture and Thread to "en-US"
            var culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public IConfiguration Configuration { get; }
        public const string ApiName = "DukkantekTask.Api";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDatabase(Configuration)
                .AddUnitOfWork()
                .AddRepositories()
                .AddBusinessServices()
                .AddActionFilters()
                .AddAutoMapper();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = ApiName,
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // configure serilog services
            ConfigureSerilog(ApiName, Configuration);
            loggerFactory.AddSerilog(Log.Logger, true);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName);
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static void ConfigureSerilog(string appName, IConfiguration configuration)
        {
            var level = configuration.GetValue<string>("Logging:LogLevel:Default");

            if (!Enum.TryParse<Serilog.Events.LogEventLevel>(level, out var logLevel))
            {
                logLevel = Serilog.Events.LogEventLevel.Information;
            }

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", appName)
                .Enrich.WithProperty("Execution", Guid.NewGuid())
                .WriteTo.Console()
                .CreateLogger();

            Log.Information($"Started {appName}");
        }

    }
}
