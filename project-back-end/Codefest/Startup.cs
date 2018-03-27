using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace Codefest
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Settings.json", false, true) 
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CodefestContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(); 

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("api", new Info
                {
                    Title = typeof(Startup).Namespace
                });
            });
        }

        public void ConfigureLogging(ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole()
                .CreateLogger();

            loggerFactory.AddSerilog();

            applicationLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }

        public void ConfigureContext(CodefestContext context)
        {
            context.Database.EnsureCreatedAsync();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment environment, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            ConfigureContext(applicationBuilder.ApplicationServices.GetService<CodefestContext>());
            ConfigureLogging(loggerFactory, applicationLifetime);

            applicationBuilder.UseMvc(options => options.MapRoute("default", "{controller}/{id?}"));
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/api/swagger.json", typeof(Startup).Namespace); });
        }
    }
}