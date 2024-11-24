using System.Collections.Generic;
using System.Threading.Tasks;
using VimaV2.DTOs;
using VimaV2.Models;

namespace VimaV2.Services
{
    public interface ICarrinhoService
    {
        Task<List<CarrinhoDTO>> GetAllItemsAsync(); // Alterado para List<CarrinhoDTO>
        Task<Carrinho> AddItemAsync(Carrinho carrinho); // Agora retorna o carrinho criado
        Task<CarrinhoDTO> GetByIdAsync(int id);
        Task<bool> RemoveItemAsync(int id);
        Task<bool> UpdateItemAsync(Carrinho carrinho);
    }


}
