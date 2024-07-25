using ECommerce.OrderService.Data;
using ECommerce.OrderService.Extensions;
using ECommerce.OrderService.Migrations.Migrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(
    builder => builder
        .AddPostgres()
        .ScanIn(typeof(SqlMigration).Assembly).For.Migrations())
    .AddOptions<ProcessorOptions>()
    .Configure(options =>
    {
        options.ProviderSwitches = "Force Quote=false";
        options.Timeout = TimeSpan.FromMinutes(2);
        options.ConnectionString = builder.Configuration["ConnectionString"];
    });

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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

app.RunWithMigrate(args);

