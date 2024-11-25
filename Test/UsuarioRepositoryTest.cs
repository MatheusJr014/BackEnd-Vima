using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VimaV2.Models;
using Microsoft.EntityFrameworkCore;
using VimaV2.Repositories;
using VimaV2.Repositories.Data;
using Xunit;

namespace Test
{
    public class UsuarioRepositoryTests
    {
        private DbContextOptions<VimaV2DbContext> _options;

        public UsuarioRepositoryTests()
        {
            // Configura o banco de dados em memória
            _options = new DbContextOptionsBuilder<VimaV2DbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task AddAsync_ShouldSaveUsuarioToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VimaV2DbContext>()
                .UseInMemoryDatabase(databaseName: "UsuarioTestDb")
                .Options;

            using var context = new VimaV2DbContext(options);
            var repository = new UsuarioRepository(context);

            var usuario = new Usuario
            {
                Nome = "Test",
                Sobrenome = "User", // Campo obrigatório
                Email = "test.user@example.com",
                Senha = "12345", // Campo obrigatório
                Role = "Admin" // Campo obrigatório
            };

            // Act
            var result = await repository.AddAsync(usuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Nome);
            Assert.Equal("User", result.Sobrenome);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectUsuario()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VimaV2DbContext>()
                .UseInMemoryDatabase(databaseName: "UsuarioTestDb")
                .EnableSensitiveDataLogging() // Para ajudar no debug
                .Options;

            using var context = new VimaV2DbContext(options);
            var repository = new UsuarioRepository(context);

            // Adicione um usuário com todos os campos obrigatórios
            var usuario = new Usuario
            {
                Nome = "John",
                Sobrenome = "Doe", // Campo obrigatório
                Email = "john.doe@example.com",
                Senha = "password123", // Campo obrigatório
                Role = "User" // Campo obrigatório
            };

            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetByIdAsync(usuario.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.Nome);
            Assert.Equal("Doe", result.Sobrenome);
            Assert.Equal("john.doe@example.com", result.Email);
            Assert.Equal("password123", result.Senha);
            Assert.Equal("User", result.Role);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnCorrectUsuario()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VimaV2DbContext>()
                .UseInMemoryDatabase(databaseName: "UsuarioTestDb")
                .EnableSensitiveDataLogging() // Opcional: ajuda no debug
                .Options;

            using var context = new VimaV2DbContext(options);
            var repository = new UsuarioRepository(context);

            // Adicione um usuário com todos os campos obrigatórios
            var usuario = new Usuario
            {
                Nome = "John",
                Sobrenome = "Doe", // Campo obrigatório
                Email = "john.doe@example.com",
                Senha = "password123", // Campo obrigatório
                Role = "User" // Campo obrigatório
            };

            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetByEmailAsync("john.doe@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.Nome);
            Assert.Equal("Doe", result.Sobrenome);
            Assert.Equal("john.doe@example.com", result.Email);
            Assert.Equal("password123", result.Senha);
            Assert.Equal("User", result.Role);
        }
    }
}
