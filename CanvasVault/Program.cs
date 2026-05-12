using CanvasVault.Application.Queries;
using CanvasVault.Domain.Interfaces;
using CanvasVault.Infrastructure;
using CanvasVault.Infrastructure.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;


namespace CanvasVault
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Basic API Services
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(); // Required for API documentation

			// Database Configuration (Infrastructure Layer)
			builder.Services.AddDbContext<CanvasVaultDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			// Repository Registrations (Infrastructure -> Domain)
			// We register the interface (Domain) with its implementation (Infrastructure)
			builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
			builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();

			// --- 4. MediatR Registration (Application Layer) 
			builder.Services.AddMediatR(cfg =>
			{
				// This tells MediatR to look for Handlers in the Application project
				cfg.RegisterServicesFromAssembly(typeof(GetAllCollectionsQuery).Assembly);

				// cfg.AddOpenBehavior(typeof(ValidationBehavior<,>)); 
			});

			// Mapster Configuration
			// Replacing AutoMapper with Mapster as requested to ensure entities aren't exposed 
			var config = TypeAdapterConfig.GlobalSettings;
			builder.Services.AddSingleton(config);
			builder.Services.AddScoped<IMapper, ServiceMapper>();

			//builder.Services.AddAuthentication().AddJwtBearer();
			//builder.Services.AddAuthorization();

			var app = builder.Build();

			//Configure the HTTP Request Pipeline 
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger(); // Enabled and accessible at /swagger
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			//Authentication must be called BEFORE Authorization
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
