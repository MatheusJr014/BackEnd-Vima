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
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
