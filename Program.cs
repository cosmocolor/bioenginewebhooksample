
using Serilog;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

using Microsoft.AspNetCore.Mvc.Authorization;

namespace EjemploWebhookBioengine
{
    public class Program
    {
         public static void Main(string[] args)
        {
            
            var enviroment = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json";
            Console.WriteLine("Env=" + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            
           
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: false);
              

            builder.Configuration.AddEnvironmentVariables();
            builder.Services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition=JsonIgnoreCondition.WhenWritingNull;
            });


            // Add services to the container.
           
            builder.Services.AddEndpointsApiExplorer(); ;

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"BioEngine API",
                    Version = "1.0",
                    Description = "API principal para conectarse a los motores biométricos",
                    Contact = new OpenApiContact { Email = "info@cosmocolor.com.mx", Name = "" }
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "EjemploWebhookBioengine.xml");
                c.IncludeXmlComments(xmlPath);
                c.ResolveConflictingActions(x => x.First());
            });
           

            
            
            builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
            
         
            var app = builder.Build();
            app.UseAuthentication();
            app.MapControllers();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = $"api-docs/{{documentName}}/swagger.json";
                
            });
            
           


            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthorization();

            
            app.UseSwaggerUI(c=>
            {

               c.SwaggerEndpoint("../api-docs/v1/swagger.json", "Bioengine V3");
                
                c.DocExpansion(DocExpansion.None);


            });
            
            app.Run();

        }
    }
}
