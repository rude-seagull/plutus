using Plutus.Api.Extensions;
using Plutus.Application;
using Plutus.Infrastructure;
using Plutus.Infrastructure.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// LOGGING
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// SERVICES
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureLogging();
builder.Services.AddControllers();

// APP
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.EnsureDatabaseCreated();
    app.SeedIdentityDataAsync();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plutus.Api v1"));
}

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();