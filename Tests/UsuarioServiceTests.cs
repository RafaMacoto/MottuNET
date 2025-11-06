using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MottuNET.data;
using MottuNET.DTOs.Usuario;
using MottuNET.Services;
using Xunit;

namespace MottuNET.Tests
{
    public class UsuarioServiceTests
    {
        private async Task<AppDbContext> GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnUsuarios()
        {
            var context = await GetEmptyDbContext();
            var service = new UsuarioService(context);

            
            var usuario = await service.CreateAsync(new UsuarioRequestDTO("Rafael", "rafael@test.com", "123456"));

            var usuarios = await service.GetAllAsync();

            Assert.Single(usuarios);
            Assert.Equal("Rafael", usuarios.First().Nome);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddUsuario()
        {
            var context = await GetEmptyDbContext();
            var service = new UsuarioService(context);

            var dto = new UsuarioRequestDTO("Novo Usuario", "novo@test.com", "senha");
            var result = await service.CreateAsync(dto);

            Assert.Equal("Novo Usuario", result.Nome);
            Assert.Equal("novo@test.com", result.Email);

            var usuarios = await service.GetAllAsync();
            Assert.Single(usuarios);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveUsuario()
        {
            var context = await GetEmptyDbContext();
            var service = new UsuarioService(context);

           
            var usuario = await service.CreateAsync(new UsuarioRequestDTO("Teste", "teste@test.com", "123"));

            var deleted = await service.DeleteAsync(usuario.Id);

            Assert.True(deleted);

            var usuarios = await service.GetAllAsync();
            Assert.Empty(usuarios); 
        }
    }
}
