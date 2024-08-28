using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vera.Infrastructure;
using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Authentication.Negotiate;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(

    options => options.TokenValidationParameters = new TokenValidationParameters
    {
        
        ValidateIssuer = true,
        
        ValidIssuer = AuthOptions.ISSUER,
        
        ValidateAudience = true,
        
        ValidAudience = AuthOptions.AUDIENCE,
        
        ValidateLifetime = true,
        
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        
        ValidateIssuerSigningKey = true,
    });

/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/account/startpage");
                    //options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });*/

//var config = new ConfigurationBuilder().Build();

//var connectionString = config.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<VeraContext>(ServiceLifetime.Scoped);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<VeraContext>().AddDefaultTokenProviders();


builder.Services.AddSession();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=account}/{action=startpage}/{id?}");

app.Run();

public class AuthOptions
{

    //------------------------------------------
    //------------------------------------------
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient";
    const string KEY = "mysupersecret_secretkey!123";
    public static SymmetricSecurityKey GetSymmetricSecurityKey(){

        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }          

    //------------------------------------------
    //------------------------------------------            
}