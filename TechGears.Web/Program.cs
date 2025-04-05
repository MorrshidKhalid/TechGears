using Microsoft.AspNetCore.Authentication.Cookies;
using TechGears.Web.Service;
using TechGears.Web.Service.IService;
using TechGears.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

SD.AuthAPIBase = builder.Configuration["ServicesUrls:AuthAPI"];
SD.CustomerAPIBase = builder.Configuration["ServicesUrls:CustomerAPI"];
SD.LeadManagmentAPIBase = builder.Configuration["ServicesUrls:LeadManagmentAPI"];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient<ILeadService, LeadServiceImpl>();
builder.Services.AddHttpClient<IAuthService, AuthServiceImpl>();
builder.Services.AddHttpClient<IAssignRole, AssignRoleImpl>();

builder.Services.AddScoped<IBaseService, BaseServiceImpl>();
builder.Services.AddScoped<ILeadService, LeadServiceImpl>();
builder.Services.AddScoped<IAuthService, AuthServiceImpl>();
builder.Services.AddScoped<IAssignRole, AssignRoleImpl>();
builder.Services.AddScoped<ITokenProvider, TokenProviderImpl>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
