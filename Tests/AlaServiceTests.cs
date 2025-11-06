using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MottuNET.data;
using MottuNET.DTOs.Ala;
using MottuNET.Models;
using MottuNET.Services;
using Xunit;

namespace MottuNET.Tests
{
    public class AlaServiceTests
    {
        private async Task<AppDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            
            var alas = new[]
            {
                new Ala { Nome = "Disponível" },
                new Ala { Nome = "Manutenção" },
                new Ala { Nome = "Recuperação" },
                new Ala { Nome = "Indisponível" }
            };
            context.Alas.AddRange(alas);
            await context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAlas()
        {
            var context = await GetDbContext();
            var service = new AlaService(context);

            var alas = await service.GetAllAsync();
            Assert.Equal(4, alas.Count());
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveAla()
        {
            var context = await GetDbContext();
            var service = new AlaService(context);

            var ala = await context.Alas.FirstAsync(a => a.Nome == "Recuperação");
            var deleted = await service.DeleteAsync(ala.Id);

            Assert.True(deleted);

            var alas = await service.GetAllAsync();
            Assert.Equal(3, alas.Count());
        }
    }
}
