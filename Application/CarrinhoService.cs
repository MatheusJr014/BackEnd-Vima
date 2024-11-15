using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Repositories;

namespace VimaV2.Services
{
    public class CarrinhoService
    {
        private readonly CarrinhoRepository _carrinhoRepository;

        public CarrinhoService(CarrinhoRepository carrinhoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
        }

        // Get All Carrinhos
        public List<CarrinhoDTO> GetAll()
        {
            var carrinhos = _carrinhoRepository.GetAll();

            return carrinhos.Select(c => new CarrinhoDTO
            {
                Id = c.Id,
                Quantidade = c.Quantidade,
                Tamanhos = c.Tamanhos,
                Product = c.Product,
                Preco = c.Preco,
                ImageURL = c.ImageURL
            }).ToList();
        }

        // Adicionar Carrinho
        public async Task<Carrinho> CreateCarrinhoAsync(Carrinho carrinho)
        {
            if (carrinho == null)
            {
                throw new ArgumentNullException(nameof(carrinho), "Carrinho não pode ser nulo.");
            }

            await _carrinhoRepository.AddCarrinhoAsync(carrinho);
            return carrinho;
        }

        // Get Carrinho by ID
        public async Task<Carrinho?> GetByIdAsync(int id)
        {
            return await _carrinhoRepository.GetByIdAsync(id);
        }


        // Delete Carrinho by ID
        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _carrinhoRepository.DeleteAsync(id);
        }


        // Atualizar carrinho
        public async Task<bool> UpdateCarrinhoAsync(int id, Carrinho carrinho)
        {
            var carrinhoExistente = await _carrinhoRepository.GetByIdAsync(id);
            if (carrinhoExistente == null)
            {
                return false;
            }

            // Atualiza as propriedades válidas
            if (carrinho.Quantidade != default(int) && carrinho.Quantidade > 0)
            {
                carrinhoExistente.Quantidade = carrinho.Quantidade;
            }

            if (!string.IsNullOrWhiteSpace(carrinho.Tamanhos))
            {
                carrinhoExistente.Tamanhos = carrinho.Tamanhos;
            }

            if (!string.IsNullOrWhiteSpace(carrinho.Product))
            {
                carrinhoExistente.Product = carrinho.Product;
            }

            if (carrinho.Preco != default(decimal) && carrinho.Preco > 0)
            {
                carrinhoExistente.Preco = carrinho.Preco;
            }

            if (!string.IsNullOrWhiteSpace(carrinho.ImageURL))
            {
                carrinhoExistente.ImageURL = carrinho.ImageURL;
            }

            return await _carrinhoRepository.UpdateAsync(carrinhoExistente);
        }
    }
}
