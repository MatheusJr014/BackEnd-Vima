using Microsoft.EntityFrameworkCore;
using VimaV2.Models;
using VimaV2.Repositories.Data;

namespace VimaV2.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly VimaV2DbContext _dbContext;

        public CarrinhoRepository(VimaV2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // [GET TODOS OS ITENS DO CARRINHO]
        public async Task<List<Carrinho>> GetAllAsync()
        {
            return await _dbContext.Carrinhos.ToListAsync();
        }

        // [ADICIONAR CARRINHO]
        public async Task AddCarrinhoAsync(Carrinho carrinho)
        {
            _dbContext.Carrinhos.Add(carrinho);
            await _dbContext.SaveChangesAsync();
        }

        // [GET CARRINHO POR ID]
        public async Task<Carrinho?> GetByIdAsync(int id)
        {
            return await _dbContext.Carrinhos.FindAsync(id);
        }

        // [DELETE CARRINHO POR ID]
        public async Task<bool> DeleteAsync(int id)
        {
            var carrinho = await _dbContext.Carrinhos.FindAsync(id);
            if (carrinho == null) return false;

            _dbContext.Carrinhos.Remove(carrinho);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // [ATUALIZAR CARRINHO]
        public async Task<bool> UpdateAsync(Carrinho carrinho)
        {
            var carrinhoExistente = await _dbContext.Carrinhos.FindAsync(carrinho.Id);
            if (carrinhoExistente == null)
            {
                return false;  // Se o carrinho não existe, não pode atualizar
            }

            // Atualize os campos do carrinho
            carrinhoExistente.Quantidade = carrinho.Quantidade;
            carrinhoExistente.Tamanhos = carrinho.Tamanhos;
            carrinhoExistente.Product = carrinho.Product;
            carrinhoExistente.Preco = carrinho.Preco;
            carrinhoExistente.ImageURL = carrinho.ImageURL;

            _dbContext.Carrinhos.Update(carrinhoExistente);
            await _dbContext.SaveChangesAsync();

            return true;  // Retorna true se a atualização foi bem-sucedida
        }

    }
}
