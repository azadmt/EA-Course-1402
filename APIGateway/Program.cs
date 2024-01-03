using APIGateway.Common;
using System.Reflection;

namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<UserClaim>();
            //builder.Services.AddControllers();
            builder.Services.AddControllers(config =>
            {
                // config.Filters.Add<LogActionFilter>(int.MinValue);
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.Use(async (context, next) =>
            {
                // string authorizationToken;
                context.Request.Headers.TryGetValue("token", out var authorizationToken);
                // Do work that can write to the Response.
                if (authorizationToken.Any())
                {
                    var token = authorizationToken.First();
                    // check token with Identity Server
                    var claim = (UserClaim)context.RequestServices.GetService(typeof(UserClaim));
                    claim.Set(new IdentityService().GetSecurityContext(token));
                }
                await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}