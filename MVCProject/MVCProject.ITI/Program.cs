using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Mapper;

namespace MVCProject.ITI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        
        builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorPages();

        //register Services&Repos in IOC container
        builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        //Register AutoMapper
        builder.Services.AddAutoMapper(options => options.AddProfile(new DomainProfile()));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }
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
