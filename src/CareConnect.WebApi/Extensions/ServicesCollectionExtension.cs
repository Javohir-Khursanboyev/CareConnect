using CareConnect.Service.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CareConnect.WebApi.Extensions;

public static class ServicesCollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
       
    }

    public static void AddValidators(this IServiceCollection services)
    {

    }

    //public static void AddExceptionHandlers(this IServiceCollection services)
    //{
    //    services.AddExceptionHandler<NotFoundExceptionHandler>();
    //    services.AddExceptionHandler<AlreadyExistExceptionHandler>();
    //    services.AddExceptionHandler<ArgumentIsNotValidExceptionHandler>();
    //    services.AddExceptionHandler<CustomExceptionHandler>();
    //    services.AddExceptionHandler<InternalServerExceptionHandler>();
    //}

    public static void AddInjectHelper(this WebApplication serviceProvider)
    {
        //var scope = serviceProvider.Services.CreateScope();
        //InjectHelper.RolePermissionService = scope.ServiceProvider.GetRequiredService<IRolePermissionService>();
    }

    public static void InjectEnvironmentItems(this WebApplication app)
    {
        HttpContextHelper.ContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
        EnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");
        EnvironmentHelper.JWTKey = app.Configuration.GetSection("JWT:Key").Value;
        EnvironmentHelper.TokenLifeTimeInHours = app.Configuration.GetSection("JWT:LifeTime").Value;
        EnvironmentHelper.EmailAddress = app.Configuration.GetSection("Email:EmailAddress").Value;
        EnvironmentHelper.EmailPassword = app.Configuration.GetSection("Email:Password").Value;
        EnvironmentHelper.SmtpPort = app.Configuration.GetSection("Email:Port").Value;
        EnvironmentHelper.SmtpHost = app.Configuration.GetSection("Email:Host").Value;
        EnvironmentHelper.PageSize = Convert.ToInt32(app.Configuration.GetSection("PaginationParams:PageSize").Value);
        EnvironmentHelper.PageIndex = Convert.ToInt32(app.Configuration.GetSection("PaginationParams:PageIndex").Value);
    }

    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
    }
}
