using System.Reflection;
using GeolocationApp.Api.Middleware;
using GeolocationApp.Application.Configuration;
using GeolocationApp.Application.Interfaces;
using GeolocationApp.Application.Services;
using GeolocationApp.Domain.Repositories;
using GeolocationApp.Infrastructure.ExternalServices;
using GeolocationApp.Infrastructure.Persistence;
using GeolocationApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace GeolocationApp.Api
{
    public class Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
            });
            services.AddControllers();
            services.AddOpenApi();
            services.AddHttpClient();
            services.Configure<GeolocationSetting>(configuration.GetSection("GeolocationSetting"));
            services.AddEndpointsApiExplorer();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load));

            services.AddSingleton<IGeolocationService, GeolocationService>();
            services.AddSingleton<IExternalApiService, ExternalApiService>();

            // Here are all the dependency injections
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseMiddleware<HttpHandleExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                if (env.IsDevelopment())
                {
                    endpoints.MapOpenApi();
                    endpoints.MapScalarApiReference(options => { options.Title = "API Geolocation V1.0.0"; });
                }
            });
        }
    }
}