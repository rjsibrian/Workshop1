using Microsoft.EntityFrameworkCore;
using AppApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
        .AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlServer(@"Server=LAPTOPCHAR\SQLEXPRESS;Database=AdventureWorks2022;Integrated Security=True;TrustServerCertificate=True;"));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
