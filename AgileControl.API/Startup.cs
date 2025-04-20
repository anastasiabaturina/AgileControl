using AgileControl.API.Middlewaries;
using AgileControl.Domain.Entities;
using AgileControl.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AgileControl.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
        Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
    )
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, LogLevel.Information));
    
    services.AddScoped<ApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
    
    services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "HotShop",
            });

        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = Configuration["JwtIssuer"],
               ValidAudience = Configuration["JwtAudience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
           };
       });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandingMiddleware>();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotShop");
        });
    }
}