using HotelRoomBookingAPI.AuthModels;
using HotelRoomBookingAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Options;
using log4net;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace SecureTradeCoreRestAPI;
/// <summary>
/// Main entry point for the REST API application.
/// </summary>
public class Program
{
    private static readonly ILog logger = LogManager.GetLogger("Program.main method");

    /// <summary>
    /// Main method to start the application.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    public static void Main(string[] args)
    {
        logger.Info("REST API Started!");

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<GuestsBookingsRepo>(); // Register GuestsBookingsRepo as a scoped service

        // Inject Application Settings             
        builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

        builder.Services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

        // Use the Add() method of IServiceCollection instance to register a service with an IoC container. 
        builder.Services.AddDbContext<HotelRoomBookingAPI.Models.LP_HotelRoomBooking_EFDBContext>(
           options =>
           {
               options.UseSqlServer(builder.Configuration.GetConnectionString("CRUDConnection"));
           });

        builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationContext>();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false; // For development only
            options.Password.RequireLowercase = false; // For development only
            options.Password.RequireUppercase = false; // For development only
            options.Password.RequireNonAlphanumeric = false; // For development only
            options.Password.RequiredLength = 6; // 12 for production
        });

        // Generatation and Validation of JWT for user Authentication
        string tmpKeyIssuer = builder.Configuration.GetSection("ApplicationSettings:JWT_Site_URL").Value;
        string tmpKeySign = builder.Configuration.GetSection("ApplicationSettings:SigningKey").Value;
        var key = Encoding.UTF8.GetBytes(tmpKeySign);

        // Use AddAuthentication method to register the service used for authentication and returns an AuthenticationBuilder instance to further configure the authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = tmpKeyIssuer,
                    ValidAudience = tmpKeyIssuer,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization using the Bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string[] {}
                    }
                });
        });

        // Apply CORS policies to specific client and endpoints
        builder.Services.AddCors(options =>
        {
        options.AddPolicy("AllowOrigin", 
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                   // policy.WithOrigins("http://127.0.0.1:4200")
                    //policy.WithOrigins("http://localhost:5180")
                   // policy.WithOrigins("https://localhost:7168")
                        .AllowAnyHeader()
                        .AllowAnyMethod(); 

                    //.WithMethods("POST", "PUT", "DELETE", "GET");
                });
        });


        ////http://127.0.0.1:5500 web app
        ////http://localhost:4200 Angular

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowOrigin");
        }

        app.UseHttpsRedirection();

       
        // Enable Cors Globally
        //app.UseCors("AllowOrigin");

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}

