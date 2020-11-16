
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Subscription.Core.Domain;
using Subscription.Core.Security.Tokens;
using Subscription.Core.Uow;
using Subscription.Data;
using Subscription.Data.Configurations;
using Subscription.Data.Uow;
using Subscription.Infrastructure.Exceptions.Service;
using Subscription.Service.Services.AuthenticationService;
using Subscription.Service.Services.BookService;
using Subscription.Service.Services.SubscriptionTypeService;
using Subscription.Service.Services.UserService;
using System;

namespace Subscription.Service.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Subscription.Data")));

            //services.AddIdentity<User, Role>()
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();
            services.AddIdentity<User, Role>()
            .AddUserStore<UserStore<User, Role, ApplicationDbContext, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>>()
            .AddRoleStore<RoleStore<Role, ApplicationDbContext, int, UserRole, IdentityRoleClaim<int>>>()
            .AddDefaultTokenProviders();

            services.AddScoped<IExceptionService, ExceptionService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<ITokenHandler, Data.Security.Tokens.TokenHandler>();

            services.Configure<Subscription.Data.Security.Tokens.TokenOptions>(configuration.GetSection("TokenOptions"));
            var tokenOptions = configuration.GetSection("TokenOptions").Get<Subscription.Data.Security.Tokens.TokenOptions>();

            var signingConfigurations = new SigningConfig(tokenOptions.Secret);
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = signingConfigurations.SecurityKey,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
