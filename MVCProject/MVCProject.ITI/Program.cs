using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.DataAccessLayer.Rpository.TripRepo;
using MVCProject.ITI.Services;
using MVCProject.ITI.Mapper;
using MVCProject.ITI.Serviceslayer.Trip;
using MVCProject.ITI.Serviceslayer;

namespace MVCProject.ITI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(
                Path.Combine(builder.Environment.ContentRootPath, "DataProtectionKeys")))
            .SetApplicationName("SmartTrip");

        // unconfirmed users cannot log in
        builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromDays(1);
        });

        //unauthenticated users redirection
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });

        builder.Services.AddControllersWithViews()
            .AddViewOptions(options =>
            {
                options.HtmlHelperOptions.ClientValidationEnabled = true;
            });

        builder.Services.AddRazorPages();

        // Single Scoped registration
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<ITripRepo, TripRepo>();
        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.AddScoped<VehicleService>();
        builder.Services.AddScoped<CarModelService>();
        builder.Services.AddTransient<IAnalyticsService, AnalyticsService>();

        builder.Services.AddAutoMapper(options => options.AddProfile(new DomainProfile()));

        // WeatherSevice on Trip
        builder.Services.AddHttpClient<IWeatherService, WeatherService>();

        // Route Trip Service
        builder.Services.AddHttpClient<IRouteService, RouteService>();

        // Trip Cost Services
        builder.Services.AddScoped<ITripCostService, TripCostService>();
        builder.Services.AddScoped<IRecentTripService, RecentTripService>();
        builder.Services.AddScoped<IUserSettingsService, UserSettingsService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
            app.UseMigrationsEndPoint();
        else
            app.UseExceptionHandler("/Home/Error");

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}