using TechGears.Web.Service;
using TechGears.Web.Service.IService;
using TechGears.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

SD.AuthAPIBase = builder.Configuration["ServicesUrls:AuthAPI"];
SD.CustomerAPIBase = builder.Configuration["ServicesUrls:CustomerAPI"];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient<ILeadService, LeadServiceImpl>();
builder.Services.AddScoped<IBaseService, BaseServiceImpl>();
builder.Services.AddScoped<ILeadService, LeadServiceImpl>();

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
