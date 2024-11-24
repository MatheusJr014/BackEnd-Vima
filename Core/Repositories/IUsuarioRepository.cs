using System.Collections.Generic;
using System.Threading.Tasks;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync(); // Alterado para assíncrono
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario> GetByIdAsync(int id); // Alterado para assíncrono
        Task<Usuario> GetByEmailAsync(string email);
    }
}
