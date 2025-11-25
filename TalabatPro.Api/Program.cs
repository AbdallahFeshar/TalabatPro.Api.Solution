using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities.IdentityModule;
using TalabatPro.Api.Core.Repository.Contract;
using TalabatPro.Api.Core.Service.Contract;
using TalabatPro.Api.Errors;
using TalabatPro.Api.Helpers;
using TalabatPro.Api.MiddleWare;
using TalabatPro.Api.Repository;
using TalabatPro.Api.Repository.Data;
using TalabatPro.Api.Repository.IdentityData;
using TalabatPro.Api.Repository.IdentityData.SeedingIdentity;
using TalabatPro.Api.Service;

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
            // Connection With DatabaseIdentity
            builder.Services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            // Add Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 5;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<IdentityContext>();


            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))

                    };
                });

            // Connection With Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var connection=builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
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
            builder.Services.AddScoped<ITokenService, TokenService>();

            //Authorize With swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TalabatPro API", Version = "v1" });

                // ?? JWT Auth
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter your JWT token like: Bearer {token}"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
            });



            // Auto Mapper Configuration
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            var app = builder.Build();

            using var scope=app.Services.CreateScope();
            var services=scope.ServiceProvider;
            var _dbContext=services.GetRequiredService<StoreContext>();
            var _IdentityDbContext = services.GetRequiredService<IdentityContext>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var loggerFactory =services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await _IdentityDbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);
                var userManger = services.GetRequiredService<UserManager<AppUser>>();
                await RoleSeeder.SeedRoles(roleManager);
                await IdentityContextSeeding.SeedUser(userManger);
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
