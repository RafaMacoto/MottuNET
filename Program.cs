
using Microsoft.EntityFrameworkCore;
using MottuNET.data;

namespace MottuNET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));


            builder.Services.AddControllers();

            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();          
                app.UseSwaggerUI();        
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
