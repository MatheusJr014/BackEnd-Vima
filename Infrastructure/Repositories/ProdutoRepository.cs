using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VimaV2.Models;
using VimaV2.Repositories.Data;

namespace VimaV2.Repositories
{
    public class ProdutoRepository : IProdutoRepository
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

        // Método para retornar um produto por ID
        public Produto GetById(int id)
        {
            // Obtém o produto pelo ID
            return _dbContext.Produtos.FirstOrDefault(p => p.Id == id);
        }

        // Método para encontrar um produto pelo ID (find)
        public Produto FindById(int id)
        {
            return _dbContext.Produtos.Find(id);
        }

        // Caso deseje incluir os métodos de Add, Update e Delete, descomente os códigos que você comentou na classe ProdutoRepository
        // public async Task<Produto> AddAsync(Produto produto)
        // {
        //     _dbContext.Produtos.Add(produto);
        //     await _dbContext.SaveChangesAsync();
        //     return produto;
        // }

        // public async Task<Produto> UpdateAsync(Produto produto)
        // {
        //     _dbContext.Produtos.Update(produto);
        //     await _dbContext.SaveChangesAsync();
        //     return produto;
        // }

        // public async Task<bool> DeleteAsync(int id)
        // {
        //     var produto = await _dbContext.Produtos.FindAsync(id);
        //     if (produto == null)
        //     {
        //         return false;
        //     }

        //     _dbContext.Produtos.Remove(produto);
        //     await _dbContext.SaveChangesAsync();
        //     return true;
        // }
    }
}
