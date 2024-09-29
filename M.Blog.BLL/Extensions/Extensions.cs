using M.Blog.BLL.Interfaces;
using M.Blog.BLL.Services;
using M.Blog.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace M.Blog.BLL.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var authSettings = configuration.GetSection(nameof(AuthSettings))
                .Get<AuthSettings>();

            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IPostService, PostService>();
            serviceCollection.AddScoped<ICommentService, CommentService>();
            serviceCollection.AddScoped<ITagService, TagService>();
            serviceCollection.AddScoped<JWTService>();

            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.SecretKey))

                    };
                    o.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["some-cookies"];
                            return Task.CompletedTask;
                        },
                    };
                });

            //serviceCollection.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminPolicy", policy =>
            //    {
            //        policy.RequireClaim("Admin", "true");
            //    });                
                
            //    options.AddPolicy("ModeratorPolicy", policy =>
            //    {
            //        policy.RequireClaim("Moderator", "true");
            //    });  
                
            //    options.AddPolicy("UserPolicy", policy =>
            //    {
            //        policy.RequireClaim("User", "true");
            //    });
                

            //});




            return serviceCollection;
        }
    }
}
