using Hangfire;
using Hangfire.SqlServer;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Profolio.Server.Filters;
using Profolio.Server.Models;
using Profolio.Server.Tools.Kafka;
using System.Reflection;

namespace Profolio.Server.Middleware
{
	public static class ServiceCollection
	{
		public static IServiceCollection CustomService(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
		{
			services.AddControllers(options =>
			{
				options.Filters.Add<ExceptionFilter>();
			});

			services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
			services.AddSingleton<IMongoConnectionStringProvider, MongoConnectionStringProvider>();
			services.AddSingleton(typeof(KafkaProduceHandler<,>));
			services.AddSingleton(typeof(KafkaProducer<,>));
			services.AddSingleton(typeof(KafkaConsumeHandler<,>));
			services.AddSingleton(typeof(KafkaConsumer<,>));

			services.AddDbContext<ProfolioContext>(options =>
					 options.UseSqlServer(configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"), provider => provider.EnableRetryOnFailure()));

			services.AddDIRegister(Assembly.GetExecutingAssembly()); // 註冊當前專案的所有類別

			#region Mapster
			services.AddMapster(); // Mapster全域配置
			MappingConfig.RegisterMapping(); // 自定義Mapping規則
			services.AddScoped<IMapper, ServiceMapper>(); // DI注入 IMapper
			services.AddSingleton(TypeAdapterConfig.GlobalSettings);
			#endregion

			#region Hangfire
			services.AddHangfire(config =>
					 config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
						   .UseSimpleAssemblyNameTypeSerializer()
						   .UseRecommendedSerializerSettings()
						   //.UseNLogLogProvider()
						   .UseSqlServerStorage(configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"), new SqlServerStorageOptions
						   {
							   UseRecommendedIsolationLevel = true,
							   DisableGlobalLocks = true
						   }));
			services.AddHangfireServer();
			#endregion

			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddHttpClient();// 註冊 HttpClientFactory
			services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificOrigins", policy =>
				{
					policy.WithOrigins(environment.IsDevelopment() ? "https://localhost:5173" : configuration.GetValue<string>("AllowedOrigins"))
						  .AllowAnyHeader()
						  .AllowAnyMethod()
						  .AllowCredentials();
				});
			});

			return services;
		}
	}
}