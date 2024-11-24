using System.Threading.Tasks;
using VimaV2.Models;

namespace VimaV2.Repositories
{
    public interface IAuthRepository
    {
        Task<Usuario?> GetUsuarioByEmailAsync(string email);
    }
}
