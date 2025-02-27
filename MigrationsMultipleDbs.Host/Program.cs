using Carter;
using Database.Core;
using Microsoft.EntityFrameworkCore;
using MigrationsMultipleDbs.Configuration;
using MigrationsMultipleDbs.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbProvider = builder.Configuration.GetValue("provider", "Postgres");

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<AuditableInterceptor>();

builder.Services.AddDbContext<ApplicationDbContext>((provider, options) =>
{
	var interceptor = provider.GetRequiredService<AuditableInterceptor>();

	if (dbProvider == "Postgres")
	{
		options.UseNpgsql(
			builder.Configuration.GetConnectionString("Postgres"),
			npgsqlOptions =>
			{
				npgsqlOptions.MigrationsHistoryTable("MigrationsHistory", "devtips_multiple_migrations");
				npgsqlOptions.MigrationsAssembly(typeof(Migrations.Postgres.IMarker).Assembly.GetName().Name!);
			});
	}

	if (dbProvider == "SqlServer")
	{
		options.UseSqlServer(
			builder.Configuration.GetConnectionString("SqlServer"),
			sqlServerOptions =>
			{
				sqlServerOptions.MigrationsHistoryTable("MigrationsHistory", "devtips_multiple_migrations");
				sqlServerOptions.MigrationsAssembly(typeof(Migrations.SqlServer.IMarker).Assembly.GetName().Name!);
			});
	}

	options
		.EnableSensitiveDataLogging()
		.AddInterceptors(interceptor)
		.UseSnakeCaseNamingConvention();
});

builder.Services.AddOptions<AuthConfiguration>()
	.Bind(builder.Configuration.GetSection(nameof(AuthConfiguration)));

builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapCarter();

// Create and seed database
using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	await DatabaseSeedService.SeedAsync(context);
}

await app.RunAsync();
