using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MottuNET.data;
using MottuNET.DTOs.Moto;
using MottuNET.Models;
using MottuNET.Services;
using Xunit;

namespace MottuNET.Tests
{
    public class MotoServiceTests
    {
        private async Task<AppDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            
            var alas = new[]
            {
                new Ala { Nome = "Disponível", Motos = new List<Moto>() },
                new Ala { Nome = "Manutenção", Motos = new List<Moto>() },
                new Ala { Nome = "Recuperação", Motos = new List<Moto>() },
                new Ala { Nome = "Indisponível", Motos = new List<Moto>() }
            };
            context.Alas.AddRange(alas);
            await context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task CreateAsync_ShouldAddMoto()
        {
            var context = await GetDbContext();
            var service = new MotoService(context);

            var ala = await context.Alas.FirstAsync(a => a.Nome == "Manutenção");
            var dto = new MotoRequestDTO(
                "Honda CG 160",
                StatusMoto.MANUTENCAO,
                "P1",
                "Pneu furado",
                "ABC1234",
                ala.Id
            );

            var result = await service.CreateAsync(dto);

            Assert.Equal("Honda CG 160", result.Modelo);
            Assert.Equal(StatusMoto.MANUTENCAO, result.Status);
            Assert.Equal("P1", result.Posicao);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMotos()
        {
            var context = await GetDbContext();
            var service = new MotoService(context);

            var ala = await context.Alas.FirstAsync(a => a.Nome == "Manutenção");
            await service.CreateAsync(new MotoRequestDTO(
                "Honda CG 160",
                StatusMoto.MANUTENCAO,
                "P1",
                "Pneu furado",
                "ABC1234",
                ala.Id
            ));

            var motos = await service.GetAllAsync();
            Assert.Single(motos);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveMoto()
        {
            var context = await GetDbContext();
            var service = new MotoService(context);

            var ala = await context.Alas.FirstAsync(a => a.Nome == "Manutenção");
            var moto = await service.CreateAsync(new MotoRequestDTO(
                "Honda CG 160",
                StatusMoto.MANUTENCAO,
                "P1",
                "Pneu furado",
                "ABC1234",
                ala.Id
            ));

            var deleted = await service.DeleteAsync(moto.Id);
            Assert.True(deleted);

            var motos = await service.GetAllAsync();
            Assert.Empty(motos);
        }
    }
}
