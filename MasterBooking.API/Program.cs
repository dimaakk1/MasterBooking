
using MasterBooking.BLL.DTO.Auth;
using MasterBooking.BLL.Services.Auth;
using MasterBooking.DAL.DbContext;
using MasterBooking.DAL.Entities;
using MasterBooking.DAL.Repositories.Appointments;
using MasterBooking.DAL.Repositories.Availabilitys;
using MasterBooking.DAL.Repositories.BlockedSlots;
using MasterBooking.DAL.Repositories.Generic;
using MasterBooking.DAL.Repositories.Reviews;
using MasterBooking.DAL.Repositories.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MasterBooking.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    )
                );

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IBlockedSlotRepository, BlockedSlotRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();


            builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection("Jwt"));

            var jwtSettings = builder.Configuration
                .GetSection("Jwt")
                .Get<JwtSettings>();

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = jwtSettings!.Issuer,

                            ValidAudience = jwtSettings.Audience,

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(jwtSettings.Key))
                        };
                });


            builder.Services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                 })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                await RoleSeeder.SeedRolesAsync(services);
            }

            app.MapControllers();

            app.Run();
        }
    }
}
