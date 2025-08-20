
using System;
using Microsoft.EntityFrameworkCore;

namespace ApiTest
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Configuration.AddEnvironmentVariables();

            var dbcontextString = builder.Configuration["ConnectionStrings:DefaultConnection"];
            builder.Services.AddDbContext<PersonContext>(opt =>
                opt.UseSqlServer(dbcontextString)
            );

            // Add services to the container.
            builder.Services.AddTransient<PersonRepo>();
            builder.Services.AddTransient<PersonService>();
            builder.Services.AddTransient<PersonContext>();

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

            app.UseAuthorization();


            app.MapControllers();


            // Auto migration khi start app
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PersonContext>();
                try
                {
                    // Tự động tạo database và chạy migration
                    context.Database.Migrate();

                    // Seed data nếu cần
                    await SeedData(context);
                }
                catch (Exception ex)
                {
                    // Log error nhưng không crash app
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            app.Run();
        }

        static async Task SeedData(PersonContext context)
        {
            // Seed initial data nếu table trống
            if (!context.People.Any())
            {
                context.People.AddRange(
                    new Person { Name = "Admin", Age = 10 }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
