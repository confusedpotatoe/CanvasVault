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

			// --- 1. Basic API Services ---
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(); // Required for API documentation [4]

			// --- 2. Database Configuration (Infrastructure Layer) [4] ---
			builder.Services.AddDbContext<CanvasVaultDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			// --- 3. Repository Registrations (Infrastructure -> Domain) [4, 5] ---
			// We register the interface (Domain) with its implementation (Infrastructure)
			builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
			builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();

			// --- 4. MediatR Registration (Application Layer) [5] ---
			builder.Services.AddMediatR(cfg =>
			{
				// This tells MediatR to look for Handlers in the Application project
				cfg.RegisterServicesFromAssembly(typeof(GetAllCollectionsQuery).Assembly);

				// For VG: This is where you would register your Pipeline Behaviors [6, 7]
				// cfg.AddOpenBehavior(typeof(ValidationBehavior<,>)); 
			});

			// --- 5. Mapster Configuration (Application Layer - VG Requirement) [6] ---
			// Replacing AutoMapper with Mapster as requested to ensure entities aren't exposed [6]
			var config = TypeAdapterConfig.GlobalSettings;
			builder.Services.AddSingleton(config);
			builder.Services.AddScoped<IMapper, ServiceMapper>();

			// --- 6. JWT Authentication (VG Requirement) [6, 7] ---
			//builder.Services.AddAuthentication().AddJwtBearer();
			//builder.Services.AddAuthorization();

			var app = builder.Build();

			// --- 7. Configure the HTTP Request Pipeline ---
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger(); // Enabled and accessible at /swagger [4]
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			// IMPORTANT FOR VG: Authentication must be called BEFORE Authorization [7]
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
