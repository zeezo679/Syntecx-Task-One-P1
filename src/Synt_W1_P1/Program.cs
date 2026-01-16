
using Microsoft.EntityFrameworkCore;
using Synt_W1_P1.Infrastructure;
using Synt_W1_P1.Interfaces;
using Synt_W1_P1.Options;
using Synt_W1_P1.Repository;
using Synt_W1_P1.Services.Users;
using Synt_W1_P1.Services.UserService;

namespace Synt_W1_P1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration
                .GetSection(nameof(ConnectionStrings))
                .Get<ConnectionStrings>()
                ?.DefaultConnection;



            // Add services to the container -> to be refactored later
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(connectionString)
            );

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
