using Microsoft.EntityFrameworkCore;
using VimaV2.Models;
using VimaV2.Database;

namespace VimaV2.Repositories
{
    public class AuthRepository
    {
        private readonly VimaV2DbContext _dbContext;

        public AuthRepository(VimaV2DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            return await _dbContext.Usuarios
                .Where(u => u.Email == email)
                .Select(u => new Usuario
                {
                    Id = u.Id,
                    Email = u.Email,
                    Senha = u.Senha,
                    Role = u.Role // Certifique-se de incluir o campo Role
                })
                .FirstOrDefaultAsync();
        }

    }
}
