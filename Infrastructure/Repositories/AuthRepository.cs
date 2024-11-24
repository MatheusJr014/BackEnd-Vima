using Microsoft.EntityFrameworkCore;
using VimaV2.Models;
using VimaV2.Repositories.Data;

namespace VimaV2.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly VimaV2DbContext _dbContext;

        public AuthRepository(VimaV2DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Usuario?> GetUsuarioByEmailAsync(string email)
        {
            var usuario = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            // Verifique se o usuário foi encontrado e se a senha não é null
            if (usuario != null && usuario.Senha == null)
            {
                throw new InvalidOperationException("Senha não encontrada para o usuário.");
            }

            return usuario;
        }



    }
}
