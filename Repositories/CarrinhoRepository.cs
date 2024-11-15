using Microsoft.EntityFrameworkCore;
using VimaV2.Database;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public class CarrinhoRepository
    {
        private readonly VimaV2DbContext _dbContext;

        public CarrinhoRepository(VimaV2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // [GET TODOS OS ITENS DO CARRINHO]
        public List<Carrinho> GetAll()
        {
            return _dbContext.Carrinhos.ToList();
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
            _dbContext.Entry(carrinho).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Caso a entidade não seja encontrada
                if (!await _dbContext.Carrinhos.AnyAsync(c => c.Id == carrinho.Id))
                {
                    return false;
                }

                throw;
            }
        }
    }
}
