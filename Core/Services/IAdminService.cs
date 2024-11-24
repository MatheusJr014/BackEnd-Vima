using VimaV2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using VimaV2.DTOs;

namespace VimaV2.Services
{
    public interface IAdminService
    {
        Task<List<Usuario>> GetAllUsersAsync();
        Task<Usuario> GetUserByIdAsync(int id);
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<Produto> AddProdutoAsync(Produto produto);
        Task<Produto> UpdateProdutoAsync(int id, ProdutoDTO produtoDTO);
        Task<bool> DeleteProdutoAsync(int produtoId);
        Task<Usuario> AddUsuarioAsync(Usuario usuario);
    }
}
