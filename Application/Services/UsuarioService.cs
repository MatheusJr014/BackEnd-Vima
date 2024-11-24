using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VimaV2.Application
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioDTO>> GetAllAsync() // Agora assíncrono
        {
            var usuarios = await _usuarioRepository.GetAllAsync(); // Usando GetAllAsync

            return usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Sobrenome = u.Sobrenome,
                Email = u.Email,
                Role = u.Role,
                Senha = u.Senha
            }).ToList();
        }
        public async Task<UsuarioDTO> AddUsuarioAsync(UsuarioDTO usuarioDto)
        {
            if (string.IsNullOrWhiteSpace(usuarioDto.Nome) || string.IsNullOrWhiteSpace(usuarioDto.Email))
            {
                throw new ArgumentException("Nome e Email são obrigatórios");
            }

            // Verificando se a senha está vazia e atribuindo um valor padrão se necessário
            var senha = string.IsNullOrWhiteSpace(usuarioDto.Senha) ? "senhaPadrão" : usuarioDto.Senha;

            // Verificando se o role está vazio e atribuindo "User" como valor padrão
            var role = usuarioDto.Role ?? "User";

            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Sobrenome = usuarioDto.Sobrenome,
                Email = usuarioDto.Email,
                Senha = senha,  // Usando a senha com valor verificado
                Role = role     // Usando o role com valor verificado
            };

            var usuarioCriado = await _usuarioRepository.AddAsync(usuario);

            return new UsuarioDTO
            {
                Id = usuarioCriado.Id,
                Nome = usuarioCriado.Nome,
                Sobrenome = usuarioCriado.Sobrenome,
                Email = usuarioCriado.Email
            };
        }

            public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id) // Agora assíncrono
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id); // Usando GetByIdAsync
            if (usuario == null)
                return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email
            };
        }
    }
}
