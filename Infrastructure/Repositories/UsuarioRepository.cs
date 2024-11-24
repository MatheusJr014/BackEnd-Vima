using Microsoft.EntityFrameworkCore;
using VimaV2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VimaV2.Repositories.Data;

namespace VimaV2.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly VimaV2DbContext _dbContext;

        public UsuarioRepository(VimaV2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método assíncrono para obter todos os usuários
        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        // Método assíncrono para adicionar um novo usuário
        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }

        // Método assíncrono para obter o usuário por ID
        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        // Método assíncrono para obter o usuário pelo e-mail
        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
