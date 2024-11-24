using System.Collections.Generic;
using System.Threading.Tasks;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public interface IAdminRepository
    {
        Task<List<Usuario>> GetAllUsersAsync();
        Task<Usuario> GetUserByIdAsync(int id);
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<Produto> AddAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task<bool> DeleteProdutoAsync(int produtoId);
        Task<Usuario> AddAsync(Usuario usuario);
    }
}
