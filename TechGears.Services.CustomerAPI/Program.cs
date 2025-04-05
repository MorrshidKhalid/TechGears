using Microsoft.EntityFrameworkCore;
using TechGears.Services.CustomerAPI.Data;
using TechGears.Services.CustomerAPI.SerializDeserializ;
using TechGears.Services.CustomerAPI.Service;
using TechGears.Services.CustomerAPI.Service.IService;
using TechGears.Services.CustomerAPI.Utility;
using TechGears.Services.LeadManagmentAPI.Service;

var builder = WebApplication.CreateBuilder(args);

SD.UserURL = builder.Configuration["ServicesUrls:UserAPI"];

var connectionString = builder.Configuration.GetConnectionString("DefualtConnection");
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(connectionString);
});

// Add services to the container.
builder.Services.AddHttpClient<IUserService, UserServiceImpl>();

builder.Services.AddScoped<ICustomerService, CustomerServiceImpl>();
builder.Services.AddScoped<ICustomerConvertService, CustomerConvertServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<ISerializer, SerializerImpl>();

builder.Services.AddHttpClient("User", u => u.BaseAddress =
new Uri(SD.UserURL));

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
