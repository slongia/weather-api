using Serilog;
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostingContext, configuration) =>
    configuration.ReadFrom.Configuration(hostingContext.Configuration));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


