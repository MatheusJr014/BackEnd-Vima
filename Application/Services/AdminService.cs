using VimaV2.Repositories;
using VimaV2.Models;
using System.Threading.Tasks;
using VimaV2.DTOs;
using System.Collections.Generic;
using System;

namespace VimaV2.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        // Obtém todos os usuários
        public async Task<List<Usuario>> GetAllUsersAsync()
        {
            return await _adminRepository.GetAllUsersAsync();
        }

        // Obtém usuário por ID
        public async Task<Usuario> GetUserByIdAsync(int id)
        {
            return await _adminRepository.GetUserByIdAsync(id);
        }

        // Obtém produto por ID
        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _adminRepository.GetProdutoByIdAsync(id);
        }

        // Adiciona um novo produto
        public async Task<Produto> AddProdutoAsync(Produto produto)
        {
            // Configura URL de imagem padrão, se necessário
            produto.ImageURL ??= "default-image-url.jpg";

            return await _adminRepository.AddAsync(produto);
        }

        // Atualiza um produto
        public async Task<Produto> UpdateProdutoAsync(int id, ProdutoDTO produtoDTO)
        {
            var produto = await _adminRepository.GetProdutoByIdAsync(id);

            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            // Validações da camada de serviços
            if (string.IsNullOrWhiteSpace(produtoDTO.Nome))
            {
                throw new ArgumentException("O campo 'Nome' é obrigatório.");
            }

            if (produtoDTO.Preco <= 0)
            {
                throw new ArgumentException("O campo 'Preco' deve ser maior que zero.");
            }

            if (produtoDTO.Estoque <= 0)
            {
                throw new ArgumentException("O campo 'Estoque' deve ser maior que zero.");
            }

            if (produtoDTO.Tamanhos == null || produtoDTO.Tamanhos.Count == 0)
            {
                throw new ArgumentException("O campo 'Tamanhos' não pode ser vazio.");
            }

            // Atualiza propriedades do produto
            produto.Nome = produtoDTO.Nome;
            produto.Descricao = produtoDTO.Descricao;
            produto.Preco = produtoDTO.Preco;
            produto.Estoque = produtoDTO.Estoque;
            produto.ImageURL = produtoDTO.ImageURL ?? produto.ImageURL;
            produto.Tamanhos = produtoDTO.Tamanhos;

            return await _adminRepository.UpdateAsync(produto);
        }

        // Deleta um produto
        public async Task<bool> DeleteProdutoAsync(int produtoId)
        {
            var produto = await _adminRepository.GetProdutoByIdAsync(produtoId);
            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            return await _adminRepository.DeleteProdutoAsync(produtoId);
        }

        // Adiciona um novo usuário
        public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
        {
            return await _adminRepository.AddAsync(usuario);
        }
    }
}
