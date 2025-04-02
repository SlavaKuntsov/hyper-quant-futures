using System.Diagnostics;
using System.Text;
using Futures.API.Middlewares;
using Futures.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Futures.API.Extensions;

public static class ApiExtensions
{
	public static IServiceCollection AddApi(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		
		services.AddExceptionHandler<GlobalExceptionHandler>();
		
		services.AddHttpContextAccessor();

		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(options =>
		{
			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				Description = "Введите токен JWT в формате 'Bearer {токен}'"
			});
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
					Array.Empty<string>()
				}
			});
		});
		
		return services;
	}

	public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
	{
		
		var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

		services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
			{
				options.RequireHttpsMetadata = true;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
				};
				options.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						Debug.WriteLine(context.Request.Headers.Authorization);
						Debug.WriteLine("Authentication failed: " + context.Exception.Message);
						return Task.CompletedTask;
					},
					OnTokenValidated = context =>
					{
						Debug.WriteLine("Token is valid.");
						return Task.CompletedTask;
					}
				};
			});

		services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
		services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

		services.AddCors(options =>
		{
			options.AddDefaultPolicy(policy =>
			{
				// policy.WithOrigins("https://localhost");
				policy.AllowAnyHeader();
				policy.AllowAnyMethod();
				policy.AllowCredentials();
			});
		});

		// предполагаем что есть роли User и Admin
		services.AddAuthorizationBuilder()
			.AddPolicy("AdminOnly", policy =>
			{
				policy.RequireRole("Admin");
				policy.AddRequirements(new ActiveAdminRequirement());
			})
			.AddPolicy("UserOnly", policy => policy.RequireRole("User"))
			.AddPolicy("UserOrAdmin", policy =>
			{
				policy.RequireRole("User", "Admin");
				policy.AddRequirements(new ActiveAdminRequirement());
			});
		
		services.AddScoped<IAuthorizationHandler, ActiveAdminHandler>();
		
		return services;
	}

	public static IHostBuilder AddLogging(this IHostBuilder hostBuilder)
	{
		hostBuilder.UseSerilog(
			(_, config) =>
			{
				config
					.WriteTo
					.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] {Message}{NewLine}{Exception}");
			});

		return hostBuilder;
	}
}