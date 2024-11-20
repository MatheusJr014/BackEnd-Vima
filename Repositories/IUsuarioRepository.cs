using System.Collections.Generic;
using System.Threading.Tasks;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetAll();
        Task<Usuario> AddAsync(Usuario usuario);
        Usuario GetById(int id);
        Task<Usuario> GetByEmailAsync(string email);
    }
}
