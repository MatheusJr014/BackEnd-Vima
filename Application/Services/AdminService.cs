using VimaV2.Repositories;
using VimaV2.Models;
using System.Threading.Tasks;
using VimaV2.DTOs;

namespace VimaV2.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<List<Usuario>> GetAllUsersAsync()
        {
            return await _adminRepository.GetAllUsersAsync();
        }

        public async Task<Usuario> GetUserByIdAsync(int id)
        {
            return await _adminRepository.GetUserByIdAsync(id);
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _adminRepository.GetProdutoByIdAsync(id);
        }

        public async Task<Produto> AddProdutoAsync(Produto produto)
        {
            return await _adminRepository.AddAsync(produto);
        }

        public async Task<Produto> UpdateProdutoAsync(int id, ProdutoDTO produtoDTO)
        {
            // Busca o produto pelo ID
            var produto = await _adminRepository.GetProdutoByIdAsync(id);

            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            // Atualiza as propriedades do produto com os dados de produtoDTO
            produto.Nome = produtoDTO.Nome;
            produto.Descricao = produtoDTO.Descricao;
            produto.Preco = produtoDTO.Preco;
            produto.Estoque = produtoDTO.Estoque;
            produto.Tamanhos = produtoDTO.Tamanhos;
            produto.ImageURL = produtoDTO.ImageURL;

            // Atualiza o produto no repositório
            return await _adminRepository.UpdateAsync(produto);
        }

        public async Task<bool> DeleteProdutoAsync(int produtoId)
        {
            return await _adminRepository.DeleteProdutoAsync(produtoId);
        }

        public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
        {
            return await _adminRepository.AddAsync(usuario);
        }
    }
}
