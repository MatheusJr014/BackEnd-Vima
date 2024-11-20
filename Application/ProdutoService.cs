using System.Collections.Generic;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Repositories;

namespace VimaV2.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoService(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }


        //[GET ALL]
        public List<ProdutoDTO> GetAll()
        {
            var produtos = _produtoRepository.GetAll();
            // Mapeando os dados de Produto para ProdutoDTO
            var produtoDTOs = produtos.Select(p => new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Descricao = p.Descricao,
                Estoque = p.Estoque,
                Tamanhos = p.Tamanhos,
                ImageURL = p.ImageURL
            }).ToList();

            return produtoDTOs;
        }



        //[POST] 
        /*public async Task<ProdutoDTO> CreateProdutoAsync(ProdutoDTO produtoDTO)
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

         
            var produtoCriado = await _produtoRepository.AddAsync(produto);

      
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
        */

        //[GET POR ID]

        public ProdutoDTO GetById(int id)
        {
            // Busca o produto no repositório
            var produto = _produtoRepository.GetById(id);

            if (produto == null)
            {
                
                return null;
            }

            // Mapeando o Produto para ProdutoDTO
            var produtoDTO = new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Descricao = produto.Descricao,
                Estoque = produto.Estoque,
                Tamanhos = produto.Tamanhos,
                ImageURL = produto.ImageURL
            };

            return produtoDTO;
        }

        /*
        //[DELETE PRODUTO]

        public bool DeleteProduto(int id)
        {
            var produto = _produtoRepository.FindById(id);

            // Verifica se o produto existe
            if (produto == null)
            {
                return false; // Produto não encontrado
            }


            _produtoRepository.Delete(produto);
            return true; // Produto excluído com sucesso
        }


        //[PUT PRODUTO]

        public async Task<ProdutoDTO> UpdateProdutoAsync(int id, ProdutoDTO produtoDTO)
        {
            var produtoExistente = _produtoRepository.GetById(id);

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

         
            var produtoAtualizado = await _produtoRepository.UpdateAsync(produtoExistente);

         
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
        */
    }
}
