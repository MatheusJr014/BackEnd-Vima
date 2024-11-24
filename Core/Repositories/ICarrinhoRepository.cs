using System.Threading.Tasks;
using System.Collections.Generic;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public interface ICarrinhoRepository
    {
        Task<List<Carrinho>> GetAllAsync();
        Task<Carrinho> GetByIdAsync(int id);  // Método para pegar o carrinho por ID
        Task AddCarrinhoAsync(Carrinho carrinho);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Carrinho carrinho);
    }

}
