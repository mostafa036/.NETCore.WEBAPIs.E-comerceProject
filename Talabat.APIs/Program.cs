using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using Talabat.APIs.Extenctions;
using Talabat.APIs.Extenstions;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entites.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var bulider = WebApplication.CreateBuilder(args);


            #region  Configration Services

            bulider.Services.AddControllers(); // ASP Web APIs

            // Connceted With Database 
            bulider.Services.AddDbContext<TalabatContext>(opions =>
                opions.UseSqlServer(bulider.Configuration.GetConnectionString("DefaultConnection")));

            // Connceted With Database 
            bulider.Services.AddDbContext<AppIdentityrDbContext>(opions =>
                opions.UseSqlServer(bulider.Configuration.GetConnectionString("identityConnection")));

            // Using Redis
            bulider.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var connection = ConfigurationOptions.Parse(bulider.Configuration.GetConnectionString("Redis"));

                return ConnectionMultiplexer.Connect(connection);
            });

            // Custom Services
            bulider.Services.addapplicationservicse();

            // Use Swagger Services
            bulider.Services.AddSwaggerServices();

            bulider.Services.addIdentityservises(bulider.Configuration);

            // Applay Cors
            bulider.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });


            #endregion

            var app = bulider.Build();

            #region  Applay Migration and Data Seeding 

            var services = app.Services;

     
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            //try
            //{
            //    var context = services.GetRequiredService<TalabatContext>();

            //    await context.Database.MigrateAsync();

            //    await TalabatContextSeed.SeedAsync(context, loggerFactory);

            //    var identityContext = services.GetRequiredService<AppIdentityrDbContext>();

            //    await identityContext.Database.MigrateAsync();

            //    var usermanger = services.GetRequiredService<UserManager<AppUser>>();

            //    await AppIdentityDbContextSedd.SeedUserAsyns(usermanger);

            //}
            //catch (Exception ex)
            //{
            //    var logger = loggerFactory.CreateLogger<Program>();

            //    logger.LogError(ex, " An error occured during apply Migration");
            //}

            #endregion

            #region Configure HTTP Request Piplines

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerDocument();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #endregion

            app.Run();
        }
    }
}
