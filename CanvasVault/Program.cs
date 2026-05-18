using CanvasVault.Application.Behaviors;
using CanvasVault.Application.Queries;
using CanvasVault.Domain.Interfaces;
using CanvasVault.Infrastructure;
using CanvasVault.Infrastructure.Repositories;
using CanvasVault.Infrastructure.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CanvasVault.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Basic API Services
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			// Swagger Configuration
			builder.Services.AddSwaggerGen(options =>
			{
				// Definiera säkerhetsschemat (JWT Bearer)
				options.AddSecurityDefinition("Bearer", securityScheme: new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Skriv in din token här."
				});

				// Lägg till kravet på säkerhet globalt i Swagger UI
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new List<string>()
					}
				});
			});

			builder.Services.AddCors(option =>
			{
				option.AddPolicy("allowFrontend", policy =>
				{
					policy.WithOrigins("http://localhost:5173")
						.AllowAnyHeader()
						.AllowAnyMethod();
				});
			});

			// Database Configuration (Infrastructure Layer)
			builder.Services.AddDbContext<CanvasVaultDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			// Repository Registrations (Infrastructure -> Domain)
			builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
			builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
			builder.Services.AddScoped<ITokenService, TokenService>();

			// --- 4. MediatR Registration (Application Layer) 
			builder.Services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(GetAllCollectionsQuery).Assembly);
				cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
			});

			// Mapster Configuration
			var config = TypeAdapterConfig.GlobalSettings;
			builder.Services.AddSingleton(config);
			builder.Services.AddScoped<IMapper, ServiceMapper>();

			// --- 6. JWT Authentication & RBAC (VG Requirement) ---
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = builder.Configuration["Jwt:Issuer"],
						ValidAudience = builder.Configuration["Jwt:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
					};
				});
			builder.Services.AddAuthorization();

			var app = builder.Build();

			//Configure the HTTP Request Pipeline 
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseCors("allowFrontend");

			//Authentication must be called BEFORE Authorization
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}