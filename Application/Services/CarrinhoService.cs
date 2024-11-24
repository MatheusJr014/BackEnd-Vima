using VimaV2.DTOs;
using VimaV2.Repositories;
using VimaV2.Services;

public class CarrinhoService : ICarrinhoService
{
    private readonly ICarrinhoRepository _carrinhoRepository;

    public CarrinhoService(ICarrinhoRepository carrinhoRepository)
    {
        _carrinhoRepository = carrinhoRepository;
    }

    // Retorna todos os carrinhos (ou itens de carrinho)
    public async Task<List<CarrinhoDTO>> GetAllItemsAsync()
    {
        var carrinhos = await _carrinhoRepository.GetAllAsync();
        return carrinhos.ConvertAll(c => new CarrinhoDTO
        {
            Id = c.Id,
            Quantidade = c.Quantidade,
            Tamanhos = c.Tamanhos,
            Product = c.Product,
            Preco = c.Preco,
            ImageURL = c.ImageURL
        });
    }

    // Adiciona um item ao carrinho
    public async Task<Carrinho> AddItemAsync(Carrinho carrinho)
    {
        await _carrinhoRepository.AddCarrinhoAsync(carrinho);
        return carrinho; // Retorna o carrinho para o controlador
    }

    // Remove um item do carrinho
    public async Task<bool> RemoveItemAsync(int id)
    {
        return await _carrinhoRepository.DeleteAsync(id);
    }

    // Atualiza um item do carrinho
    public async Task<bool> UpdateItemAsync(Carrinho carrinho)
    {
        var carrinhoExistente = await _carrinhoRepository.GetByIdAsync(carrinho.Id);
        if (carrinhoExistente == null)
        {
            return false;  // Se o carrinho não existe, retorna false
        }

        // Atualize o carrinho
        return await _carrinhoRepository.UpdateAsync(carrinho);
    }

    // Método para pegar o carrinho por ID
    public async Task<CarrinhoDTO> GetByIdAsync(int id)
    {
        var carrinho = await _carrinhoRepository.GetByIdAsync(id);
        if (carrinho == null)
            return null;

        return new CarrinhoDTO
        {
            Id = carrinho.Id,
            Quantidade = carrinho.Quantidade,
            Tamanhos = carrinho.Tamanhos,
            Product = carrinho.Product,
            Preco = carrinho.Preco,
            ImageURL = carrinho.ImageURL
        };
    }
}
