using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Repository.Contract;
using TalabatPro.Api.Errors;
using TalabatPro.Api.Helpers;
using TalabatPro.Api.MiddleWare;
using TalabatPro.Api.Repository;
using TalabatPro.Api.Repository.Data;

namespace TalabatPro.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Connection With Database
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            //Validation Errors Configuration
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count() > 0)
                                                        .SelectMany(e => e.Value.Errors)
                                                        .Select(e => e.ErrorMessage)
                                                        .ToList();
                    var Response = new ApiValidationError() { Errors = errors };
                    return new BadRequestObjectResult(Response);
                };
            });

            // Repositories Configuration
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));


            // Auto Mapper Configuration
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            var app = builder.Build();

            using var scope=app.Services.CreateScope();
            var services=scope.ServiceProvider;
            var _dbContext=services.GetRequiredService<StoreContext>();
            var loggerFactory=services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);
            }
            catch (Exception ex)
            {

                var logger=loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,"An error occurred during migration");
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //use static files
            app.UseStaticFiles();

            //Use Custom Exception MiddleWare
            app.UseMiddleware<ExceptionMiddleWare>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
