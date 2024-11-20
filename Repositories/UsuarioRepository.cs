using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VimaV2.Database;
using VimaV2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VimaV2.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly VimaV2DbContext _dbContext;

        public UsuarioRepository(VimaV2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para obter todos os usuários
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

        public Task<Usuario> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public Usuario GetById(int id)
        {
            throw new NotImplementedException();
        }

        // Método assíncrono para obter o usuário pelo nome de usuário
        /*public async Task<Usuario> GetByUsernameAsync(string username)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }
        */
    }
}
