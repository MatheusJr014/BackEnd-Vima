using System.Threading.Tasks;
using VimaV2.DTOs;

namespace VimaV2.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDTO loginDto);
    }
}
