using CanvasVault.Application.Commands;
using CanvasVault.Domain.Interfaces;
using CanvasVault.Infrastructure;
using CanvasVault.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CanvasVault
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Register MediatR services and handlers from the current assembly
			builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

			// Add services to the container.
			// Configure Entity Framework Core with SQL Server
			builder.Services.AddDbContext<CanvasVaultDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCollectionCommand).Assembly));

			// Register the ArtworkRepository for dependency injection
			builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
			builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();

			builder.Services.AddControllers();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			app.UseSwagger();
			app.UseSwaggerUI();
			app.MapControllers();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
