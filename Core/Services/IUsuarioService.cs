using System.Collections.Generic;
using System.Threading.Tasks;
using VimaV2.DTOs;

namespace VimaV2.Application
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> GetAllAsync(); // Agora assíncrono
        Task<UsuarioDTO> AddUsuarioAsync(UsuarioDTO usuarioDto);
        Task<UsuarioDTO> GetUsuarioByIdAsync(int id); // Agora assíncrono
    }
}
