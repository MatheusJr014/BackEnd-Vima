using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VimaV2.Application
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        // Alteração: Injeção da interface IUsuarioRepository
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // Método para obter todos os usuários
        public List<UsuarioDTO> GetAll()
        {
            var usuarios = _usuarioRepository.GetAll();

            // Corrigido o mapeamento para retornar uma lista de DTOs corretamente
            var usuarioDTOs = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Sobrenome = u.Sobrenome,
                Email = u.Email
                // A senha não deve ser incluída no DTO por questões de segurança
            }).ToList();

            return usuarioDTOs;
        }

        // Método para adicionar um novo usuário, com a Role definida como "User"
        public async Task<UsuarioDTO> AddUsuarioAsync(UsuarioDTO usuarioDto)
        {
            // Validação simples do DTO
            if (string.IsNullOrWhiteSpace(usuarioDto.Nome) || string.IsNullOrWhiteSpace(usuarioDto.Email))
                throw new ArgumentException("Nome e Email são obrigatórios");

            // Mapeamento de DTO para a entidade Usuario
            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Sobrenome = usuarioDto.Sobrenome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha, // A senha deve ser criptografada antes de ser salva
                Role = usuarioDto.Role ?? "User" // Se o Role não for fornecido, atribui "User"
            };

            // Adiciona o usuário no repositório
            var usuarioCriado = await _usuarioRepository.AddAsync(usuario);

            // Retorna o DTO com os dados do usuário criado
            return new UsuarioDTO
            {
                Id = usuarioCriado.Id,
                Nome = usuarioCriado.Nome,
                Sobrenome = usuarioCriado.Sobrenome,
                Email = usuarioCriado.Email
                // A senha não deve ser retornada no DTO
            };
        }

        // Método para obter o usuário por ID
        public UsuarioDTO GetUsuarioById(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (usuario == null)
                return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email
                // A senha não deve ser incluída no DTO
            };
        }
    }
}
