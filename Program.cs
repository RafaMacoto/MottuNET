using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using MottuNET.data;
using MottuNET.Services.Interfaces;
using MottuNET.Services;
using System.Text.Json.Serialization;

namespace MottuNET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));

            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                 {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                  });


            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mottu API",
                    Version = "v1",
                    Description = "API para gerenciamento de Motos, Alas e Usu�rios"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                }

                c.EnableAnnotations();

                c.ExampleFilters();

            });

            builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

            // seus services
            builder.Services.AddScoped<IAlaService, AlaService>();
            builder.Services.AddScoped<IMotoService, MotoService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>(); 

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu API v1");
                });
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
