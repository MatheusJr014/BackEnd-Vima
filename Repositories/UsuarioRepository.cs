using Microsoft.AspNetCore.Mvc;
using VimaV2.Database;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public class UsuarioRepository
    {
        private readonly VimaV2DbContext _dbContext; 

        public UsuarioRepository(VimaV2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Usuario> GetAll()
        {
            return _dbContext.Usuarios.ToList();
        }

        // Método assíncrono para adicionar um novo usuário
        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }


        // Método para obter o usuário por ID
        public Usuario GetById(int id)
        {
            return _dbContext.Usuarios.FirstOrDefault(u => u.Id == id);
        }
    }
}
