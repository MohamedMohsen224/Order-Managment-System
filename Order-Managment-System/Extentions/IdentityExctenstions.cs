using Core.Models.IdentityServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Reposatry.Data;
using Reposatry.Data.IdentityContext;
using Services;
using System.Runtime.CompilerServices;
using System.Text;

namespace Order_Managment_System.Extentions
{
    public static class IdentityExctenstions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<Identitycontext>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddAuthentication(OPTIONS=> { 
                OPTIONS.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                OPTIONS.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:SecretKey"])),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = false

                    };
                });

            return services;


            
            
        }
    }
}
