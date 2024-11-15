using Microsoft.AspNetCore.Mvc;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Repositories;

namespace VimaV2.Application
{
    public class UsuarioService 
    {
        private readonly UsuarioRepository _usuarioRepository; 

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        public List<UsuarioDTO> GetAll()
        {
            var usuarios = _usuarioRepository.GetAll();

            var usuarioDTOs = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Sobrenome = u.Sobrenome,
                Email = u.Email,
                Senha = u.Senha
            }).ToList();

            return usuarioDTOs;
        }


        // Método para adicionar um novo usuário
        public async Task<UsuarioDTO> AddUsuarioAsync(UsuarioDTO usuarioDto)
        {
            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Sobrenome = usuarioDto.Sobrenome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha
            };

            var usuarioCriado = await _usuarioRepository.AddAsync(usuario);

            return new UsuarioDTO
            {
                Id = usuarioCriado.Id,
                Nome = usuarioCriado.Nome,
                Sobrenome = usuarioCriado.Sobrenome,
                Email = usuarioCriado.Email,
                Senha = usuarioCriado.Senha
            };
        }


        // Método para obter o usuário por ID, chamando o repositório
        public Usuario GetUsuarioById(int id)
        {
            return _usuarioRepository.GetById(id);
        }
    }
}
