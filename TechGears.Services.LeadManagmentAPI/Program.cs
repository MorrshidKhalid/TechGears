using Microsoft.EntityFrameworkCore;
using TechGears.Services.LeadManagmentAPI.Service;
using TechGears.Services.LeadManagmentAPI.Data;
using TechGears.Services.LeadManagmentAPI.Service.IService;
using TechGears.Services.LeadManagmentAPI.Utility;
using TechGears.Services.LeadManagmentAPI.SerializDeserializ;
using TechGears.Services.CustomerAPI.Service;

var builder = WebApplication.CreateBuilder(args);

SD.CustomerURL = builder.Configuration["ServicesUrls:CustomerAPI"];
SD.UserURL = builder.Configuration["ServicesUrls:UserAPI"];

// Add services to the container.

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IUserService, UserServiceImpl>();
builder.Services.AddHttpClient<ILeadConversionService, LeadConversionService>();

builder.Services.AddScoped<ILeadService, LeadServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<ISerializer, SerializerImpl>();
builder.Services.AddScoped<ILeadConversionService, LeadConversionService>();
builder.Services.AddScoped<ILeadAssignmentService, LeadAssignmentServiceImpl>();

var connectionString = builder.Configuration.GetConnectionString("DefualtConnection");
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(connectionString);
});

builder.Services.AddHttpClient("User", u => u.BaseAddress =
new Uri(SD.UserURL));

builder.Services.AddHttpClient("Customer", c => c.BaseAddress =
new Uri(SD.CustomerURL));

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
