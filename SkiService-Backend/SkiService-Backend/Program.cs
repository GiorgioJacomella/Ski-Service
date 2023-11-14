
using Microsoft.EntityFrameworkCore;
using SkiService_Backend.Models;

namespace SkiService_Backend
{
    public class Program
    {
        public static string _connectionString;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://127.0.0.1:5500")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Build configuration for the connection string
            var configuration = builder.Configuration;
            _connectionString = configuration.GetConnectionString("Db1");
            builder.Services.AddDbContext<registrationContext>(options =>
                options.UseSqlServer(_connectionString));


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }


            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
