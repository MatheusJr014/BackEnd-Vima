using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VimaV2.Database;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public class ProdutoRepository
    {
        private readonly VimaV2DbContext _dbContext;

        public ProdutoRepository(VimaV2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para retornar todos os produtos
        public List<Produto> GetAll()
        {
            return _dbContext.Produtos.ToList();
        }

        public async Task<Produto> AddAsync(Produto produto)
        {
            _dbContext.Produtos.Add(produto);
            await _dbContext.SaveChangesAsync();
            return produto;
        }

        public Produto GetById(int id)
        {
            // Obtém o produto pelo ID
            return _dbContext.Produtos.FirstOrDefault(p => p.Id == id);
        }



        public Produto FindById(int id)
        {
            return _dbContext.Produtos.Find(id);
        }

        public void Delete(Produto produto)
        {
            _dbContext.Produtos.Remove(produto);
            _dbContext.SaveChanges();
        }



        public async Task<Produto> UpdateAsync(Produto produto)
        {
            _dbContext.Produtos.Update(produto);
            await _dbContext.SaveChangesAsync();
            return produto;
        }
    }
}
