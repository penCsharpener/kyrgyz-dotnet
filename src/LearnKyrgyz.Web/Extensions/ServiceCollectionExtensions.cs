using LearnKyrgyz.Application;
using LearnKyrgyz.Domain.Models;
using LearnKyrgyz.Persistence.Data;
using LearnKyrgyz.Web.Areas.Identity;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LearnKyrgyz.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterWebServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(builder.Configuration));

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AppUser>>();
        builder.Services.AddMediatR(typeof(AssemblyMarker).Assembly);
    }
}
