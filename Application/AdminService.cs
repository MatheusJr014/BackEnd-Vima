using System.Collections.Generic;
using System.Threading.Tasks;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Repositories;

namespace VimaV2.Services
{
    public class AdminService
    {
        private readonly AdminRepository _adminRepository;

        public AdminService(AdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        // [POST] Criar Produto (somente para Admin)
        public async Task<ProdutoDTO> CreateProdutoAsync(ProdutoDTO produtoDTO)
        {
            var produto = new Produto
            {
                Nome = produtoDTO.Nome,
                Preco = produtoDTO.Preco,
                Descricao = produtoDTO.Descricao,
                Estoque = produtoDTO.Estoque,
                Tamanhos = produtoDTO.Tamanhos,
                ImageURL = produtoDTO.ImageURL
            };

            var produtoCriado = await _adminRepository.AddAsync(produto);

            return new ProdutoDTO
            {
                Id = produtoCriado.Id,
                Nome = produtoCriado.Nome,
                Preco = produtoCriado.Preco,
                Descricao = produtoCriado.Descricao,
                Estoque = produtoCriado.Estoque,
                Tamanhos = produtoCriado.Tamanhos,
                ImageURL = produtoCriado.ImageURL
            };
        }

        // [PUT] Atualizar Produto (somente para Admin)
        public async Task<ProdutoDTO> UpdateProdutoAsync(int id, ProdutoDTO produtoDTO)
        {
            var produtoExistente = await _adminRepository.GetProdutoByIdAsync(id); // Usar o método assíncrono

            if (produtoExistente == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            produtoExistente.Nome = produtoDTO.Nome;
            produtoExistente.Preco = produtoDTO.Preco;
            produtoExistente.Descricao = produtoDTO.Descricao;
            produtoExistente.Estoque = produtoDTO.Estoque;
            produtoExistente.Tamanhos = produtoDTO.Tamanhos;
            produtoExistente.ImageURL = produtoDTO.ImageURL;

            var produtoAtualizado = await _adminRepository.UpdateAsync(produtoExistente);

            return new ProdutoDTO
            {
                Id = produtoAtualizado.Id,
                Nome = produtoAtualizado.Nome,
                Preco = produtoAtualizado.Preco,
                Descricao = produtoAtualizado.Descricao,
                Estoque = produtoAtualizado.Estoque,
                Tamanhos = produtoAtualizado.Tamanhos,
                ImageURL = produtoAtualizado.ImageURL
            };
        }

        // [DELETE] Deletar Produto (somente para Admin)
        public async Task<bool> DeleteProdutoAsync(int id)
        {
            return await _adminRepository.DeleteProdutoAsync(id); // Usar método assíncrono
        }
    }
}
