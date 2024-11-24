using Microsoft.EntityFrameworkCore;
using VimaV2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using VimaV2.Repositories.Data;

namespace VimaV2.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly VimaV2DbContext _dbContext;

        public AdminRepository(VimaV2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Usuario>> GetAllUsersAsync()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetUserByIdAsync(int id)
        {
            return await _dbContext.Usuarios.FindAsync(id);
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _dbContext.Produtos.FindAsync(id);
        }

        public async Task<Produto> AddAsync(Produto produto)
        {
            _dbContext.Produtos.Add(produto);
            await _dbContext.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            _dbContext.Produtos.Update(produto);
            await _dbContext.SaveChangesAsync();
            return produto;
        }

        public async Task<bool> DeleteProdutoAsync(int produtoId)
        {
            var produto = await _dbContext.Produtos.FindAsync(produtoId);
            if (produto != null)
            {
                _dbContext.Produtos.Remove(produto);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }
    }
}
