using Infrastructure;
using Infrastructure.Date;
using Infrastructure.Interface;
using Infrastructure.Date;
using Infrastructure.Interface;
using Serilog;
using Infrastructure.Services;
using Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DateContext>();


builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRentalService, RentalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();





;