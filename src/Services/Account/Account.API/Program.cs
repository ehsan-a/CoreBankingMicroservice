using Account.API.Extensions.ServiceCollection;
using Serilog;
using Shared.ServiceDefaults;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    //.WriteTo.File("logs/api-log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddCustomMediatR()
    .AddValidation()
    .AddInfrastructure(builder.Configuration)
    .AddApplicationServices()
    .AddCustomCors(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorizationPolicies()
    .AddSwaggerDocumentation()
    .AddRateLimiting(builder.Configuration)
    .AddCustomHealthChecks(builder.Configuration);

builder.AddApplicationServices();

var app = builder.Build();

app.UseCustomMiddlewares();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("DevCors");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();

app.MapDefaultEndpoints();

app.Logger.LogInformation("LAUNCHING Account.API");

app.Run();
